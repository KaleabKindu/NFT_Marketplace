import Link from "next/link";

import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button";
import { help_center } from "@/data";

type Props = {}

const HelpCenter = (props: Props) => {
    
  return (
    <DropdownMenu>
        <DropdownMenuTrigger className="rounded-full">
            <Button variant='ghost' className='rounded-full' size='lg'>Help Center</Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
          {
            help_center.map((option, index) => 
            <DropdownMenuItem key={index} className="py-3">
                <Link href={option.route} className="w-full">
                   {option.name}
                 </Link>
            </DropdownMenuItem>
            )
          }
        </DropdownMenuContent>
    </DropdownMenu>
  );
}

export default HelpCenter