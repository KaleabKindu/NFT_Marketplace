import Link from "next/link";

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { help_center } from "@/data";
import { MdOutlineKeyboardArrowDown } from "react-icons/md";
import { useState } from "react";
import { useRouter } from "next/navigation";

type Props = {};

const HelpCenter = (props: Props) => {
  const router = useRouter();
  const [isOpen, setIsOpen] = useState(false);

  return (
    <DropdownMenu open={isOpen} onOpenChange={(a) => setIsOpen(a)}>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="rounded-full" size="lg">
          Help Center
          <MdOutlineKeyboardArrowDown className="ml-3" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="rounded-xl">
        {help_center.map((option, index) => (
          <DropdownMenuItem
            key={index}
            className="w-[200px] px-5 py-3 cursor-pointer"
            onClick={() => router.push(option.route)}
          >
            {option.name}
          </DropdownMenuItem>
        ))}
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default HelpCenter;
