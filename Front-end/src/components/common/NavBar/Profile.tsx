import { Avatar } from "../Avatar";
import Link from "next/link";
import { FaUserAlt } from "react-icons/fa";
import { MdLogout } from "react-icons/md";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Routes } from "@/routes";
import { useAccount, useDisconnect } from "wagmi";
import useWeb3Status from "@/hooks/useWeb3Status";
import { persistor } from "@/store";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { useGetUserDetailsQuery } from "@/store/api";

type Props = {};

const Profile = (props: Props) => {
  const router = useRouter();
  const [isOpen, setIsOpen] = useState(false);
  const { disconnect } = useDisconnect();
  const { address } = useAccount();
  const { data: user } = useGetUserDetailsQuery(address as string);
  const handleLogout = () => {
    disconnect();
    persistor.purge();
  };
  return (
    <DropdownMenu open={isOpen} onOpenChange={(a) => setIsOpen(a)}>
      <DropdownMenuTrigger className="rounded-full">
        <Avatar name={user?.userName} src={user?.avatar} />
      </DropdownMenuTrigger>
      <DropdownMenuContent>
        <DropdownMenuLabel>
          <div className="flex gap-3 items-center justify-center p-3">
            <Avatar name={user?.userName} src={user?.avatar} />
            <div>
              <div className="scroll-m-20 text-xl font-semibold tracking-tight">
                Tony Stark
              </div>
              <div className="text-sm font-medium truncate max-w-[10rem]">
                {address}
              </div>
            </div>
          </div>
        </DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuItem
          className="flex items-center gap-3 w-full px-3 py-3 cursor-pointer"
          onClick={() => router.push(`${Routes.USER}/${address}`)}
        >
          <FaUserAlt size={25} />
          <div>Profile</div>
        </DropdownMenuItem>
        <DropdownMenuItem
          className="flex items-center gap-3 py-3 cursor-pointer"
          onClick={() => handleLogout()}
        >
          <MdLogout size={25} className="ml-3" />
          <div>Disconnect</div>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default Profile;
