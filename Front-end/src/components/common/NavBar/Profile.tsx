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
import { useDisconnect } from "wagmi";
import useWeb3Status from "@/hooks/useWeb3Status";
import { persistor } from "@/store";

type Props = {};

const Profile = (props: Props) => {
  const { disconnect } = useDisconnect();
  const { address } = useWeb3Status();
  const handleLogout = () => {
    disconnect();
    persistor.purge();
  };
  return (
    <DropdownMenu>
      <DropdownMenuTrigger className="rounded-full">
        <Avatar />
      </DropdownMenuTrigger>
      <DropdownMenuContent>
        <DropdownMenuLabel>
          <div className="flex gap-3 items-center justify-center p-4">
            <Avatar />
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
        <DropdownMenuItem className="py-3">
          <Link
            href={`${Routes.USER}/${address}`}
            className="flex items-center gap-3 w-full"
          >
            <FaUserAlt size={30} />
            <div>Profile</div>
          </Link>
        </DropdownMenuItem>
        <DropdownMenuItem
          className="flex items-center gap-3 py-3 cursor-pointer"
          onClick={() => handleLogout()}
        >
          <MdLogout size={30} />
          <div>Disconnect</div>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default Profile;
