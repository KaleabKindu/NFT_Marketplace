import Link from "next/link";

import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button";
import { discover } from "@/data";

type Props = {}

const Discover = (props: Props) => {
    
  return (
    <DropdownMenu>
        <DropdownMenuTrigger className="rounded-full">
            <Button variant='ghost' className='rounded-full' size='lg'>Discover</Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
            {
                discover.map((option, index) => 
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

export default Discover