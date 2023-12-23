import Link from "next/link";

import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button";

type Props = {}

const HelpCenter = (props: Props) => {
    
  return (
    <DropdownMenu>
        <DropdownMenuTrigger className="rounded-full">
            <Button variant='ghost' className='rounded-full' size='lg'>Help Center</Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
            <DropdownMenuItem className="py-3">
                <Link href='' className="w-full">
                    About Us
                 </Link>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3">
                <Link href='' className="w-full">
                    Contact Us
                 </Link>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3">
                <Link href='' className="w-full">
                    Sign In
                 </Link>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3">
                <Link href='' className="w-full">
                    Sign Up
                 </Link>
            </DropdownMenuItem>
        </DropdownMenuContent>
    </DropdownMenu>
  );
}

export default HelpCenter