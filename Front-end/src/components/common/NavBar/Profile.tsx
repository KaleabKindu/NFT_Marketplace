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
  } from "@/components/ui/dropdown-menu"
import { Routes } from "@/routes";
  

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
                            0x3849938292020338778343
                        </div>
                    </div>
                </div>
            </DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem className="py-3">
                <Link href={`${Routes.USER}/0x3849938292020338778343`} className="flex items-center gap-3 w-full">
                    <FaUserAlt size={30} />
                    <div>Profile</div>
                 </Link>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3">
                <Link href='' className="flex items-center gap-3">
                    <MdLogout size={30} />
                    <div>Disconnect</div>
                 </Link>
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
  );
}

export default Profile