import { Avatar } from "../Avatar";
import Link from "next/link";
import { FaUserAlt } from "react-icons/fa";
import { TbNetworkOff } from "react-icons/tb";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
  

type Props = {}

const Profile = (props: Props) => {
    
  return (
    <DropdownMenu>
        <DropdownMenuTrigger className="rounded-full">
            <Avatar/>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
            <DropdownMenuLabel>
                <div className="flex gap-3 items-center justify-center p-4"
                >
                    <Avatar/>
                    <div>
                        <div className="scroll-m-20 text-xl font-semibold tracking-tight">
                            Tony Stark
                        </div>
                        <div className="text-sm font-medium truncate max-w-[10rem]" >
                            X03849938292020338778343
                        </div>
                    </div>
                </div>
            </DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem>
                <Link href='' className="flex items-center gap-3">
                    <FaUserAlt size={20} />
                    <div>Profile</div>
                 </Link>
            </DropdownMenuItem>
            <DropdownMenuItem>
                <Link href='' className="flex items-center gap-3">
                    <TbNetworkOff size={20} />
                    <div>Disconnect</div>
                 </Link>
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
  );
}

export default Profile