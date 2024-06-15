"use client";
import Logo from "../Logo";
import Profile from "./Profile";
import Notifications from "./Notifications";
import { SunMoon, MoonStar, Loader2 } from "lucide-react";

import { Button } from "@/components/ui/button";
import Container from "../Container";
import SideBar from "./SideBar";
import Discover from "./Discover";
import HelpCenter from "./HelpCenter";
import Link from "next/link";
import { Routes } from "@/routes";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import useWebTheme from "@/hooks/useWebTheme";
import { useAccount, useSignMessage } from "wagmi";
import {
  useAuthenticateSignatureMutation,
  useGetNounceMutation,
} from "@/store/api";
import { Address, INotification } from "@/types";
import { useContext, useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { useToast } from "@/components/ui/use-toast";
import { ToastAction } from "@/components/ui/toast";
import { setSession } from "@/store/slice/auth";
import { useDisconnect } from "wagmi";
import { SocketContext } from "@/context/SocketContext";
import {
  addNotification,
  addNotifications,
  setUnReadcount,
} from "@/store/slice/notification";

type Props = {};

const NavBar = (props: Props) => {
  const dispatch = useAppDispatch();
  const session = useAppSelector((state) => state.auth.session);
  const { socketConnection } = useContext(SocketContext);
  const { mode, handleToggle } = useWebTheme();
  const { toast } = useToast();
  const { address, isConnected } = useAccount({
    onConnect: () => {
      if (!session) {
        signIn();
      }
    },
  });
  const [getNonce] = useGetNounceMutation();
  const [authenticateSignature] = useAuthenticateSignatureMutation();
  const [loading, setLoading] = useState(false);
  const { signMessageAsync } = useSignMessage();
  const { disconnect } = useDisconnect();
  const { open } = useWeb3Modal();
  const handleLogin = () => {
    if (isConnected) {
      signIn();
    } else {
      open();
    }
  };

  useEffect(() => {
    if (session) {
      socketConnection?.on(
        "RecieveNotification",
        (notification: INotification) => {
          console.log("RecieveNotification:", notification);
          dispatch(addNotification(notification));
        },
      );

      socketConnection?.on("LoadNotifications", (notifications) => {
        console.log("LoadNotifications", notifications);
        dispatch(addNotifications(notifications));
      });

      socketConnection?.on("UnReadNotificationCount", (count) => {
        console.log("UnReadNotificationCount", count);
        dispatch(setUnReadcount(count));
      });
    }

    return () => {
      [
        "RecieveNotification",
        "LoadNotifications",
        "UnReadNotificationCount",
      ].forEach((methodName: string) => socketConnection?.off(methodName));
    };
  }, [socketConnection]);

  const signIn = async () => {
    try {
      setLoading(true);
      const nonce = await getNonce(address as Address).unwrap();

      const signature = await signMessageAsync({ message: nonce });
      const token = await authenticateSignature({
        address: address as Address,
        signedNonce: signature,
      }).unwrap();
      dispatch(setSession(token));
    } catch (error) {
      disconnect();
      console.log(error);
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        action: <ToastAction altText="Try Again">Try Again</ToastAction>,
      });
    } finally {
      setLoading(false);
    }
  };
  return (
    <div className="sticky z-50 w-full bg-background top-0">
      <Container className="flex items-center justify-between border-b">
        {/* Left Section */}
        <Logo />

        {/* Right Section */}
        <div className="hidden lg:flex gap-5 items-center">
          {/* Discover Section */}
          <Discover />

          {/* Help Center Section */}
          <HelpCenter />

          {/* Notification Section */}
          {session && <Notifications />}

          {/* Toggle Theme  */}
          <Button
            variant="ghost"
            size={"icon"}
            className="rounded-full"
            onClick={handleToggle}
          >
            {mode === "dark" ? <SunMoon /> : <MoonStar />}
          </Button>

          {session ? (
            <Link href={Routes.MINT}>
              <Button className="rounded-full" size="lg">
                Create
              </Button>
            </Link>
          ) : (
            <Button
              disabled={loading}
              className="rounded-md"
              onClick={() => handleLogin()}
            >
              {loading ? (
                <>
                  <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                  Logging In
                </>
              ) : (
                "Connect"
              )}
            </Button>
          )}
          {/* User Profile Section */}
          {session && <Profile />}
        </div>

        {/* Mobile Sidebar */}
        <SideBar />
      </Container>
    </div>
  );
};

export default NavBar;
