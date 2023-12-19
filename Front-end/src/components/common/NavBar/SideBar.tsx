import {
  Sheet,
  SheetContent,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet"
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion"


import { useState } from 'react'
import { RiMenu3Fill } from "react-icons/ri";
import { FaUserAlt } from "react-icons/fa";
import { MdLogout } from "react-icons/md";
import { useTheme } from "next-themes";
import { Button } from '@/components/ui/button';

import Link from "next/link";

import Logo from "../Logo";
import { discover, help_center } from "@/data";
import { MoonStar, SunMoon } from "lucide-react";

type Props = {

}

const SideBar = (props: Props) => {
    const { theme, setTheme } = useTheme()
    const handleToggle = () => {
      if(theme === 'light'){
        setTheme('dark')
      }else{
        setTheme('light')
      }
    }
  return (
    <div className="lg:hidden">

          <Sheet>
            <SheetTrigger>
              <RiMenu3Fill size={30} />
            </SheetTrigger>
            <SheetContent side={'left'}>
              <SheetHeader>                  
                <SheetTitle>
                  <Logo/>
                </SheetTitle>
              </SheetHeader>
              <Accordion type="single" collapsible className="w-full mt-[1.5rem]">
                <AccordionItem value="item-1">
                  <AccordionTrigger>
                    Discover
                  </AccordionTrigger>
                  <AccordionContent>
                    <div className="flex flex-col gap-3">

                      {
                        discover.map((option, index) => 
                          <Button key={index} variant='ghost' className="justify-start px-3">
                            <Link href={option.route}>
                              {option.name}
                            </Link>
                          </Button>
                        )
                      }

                    </div>
                  </AccordionContent>
                </AccordionItem>
                <AccordionItem value="item-2">
                  <AccordionTrigger>Help Center</AccordionTrigger>
                  <AccordionContent>
                    <div className="flex flex-col gap-3">
                      {
                        help_center.map((option, index) => 
                          <Button key={index} variant='ghost' className="justify-start px-3">
                            <Link href={option.route}>
                            {option.name}
                            </Link>
                          </Button>
                        )
                      }
                    </div>
                  </AccordionContent>
                </AccordionItem>
                <AccordionItem value="item-3" className="border-b mb-[2.5rem]">
                  <AccordionTrigger>Notifications</AccordionTrigger>
                  <AccordionContent>
                    <div className="p-3">
                      <div> Measure action your user...</div>
                      <div className="text-sm font-medium leading-none">4 minutes ago</div>
                    </div>
                  </AccordionContent>
                </AccordionItem>
              </Accordion>
              <Link href='' className="flex  w-full">
                <Button variant='ghost' className='w-full gap-3 justify-start items-center px-3'>
                    <FaUserAlt size={25} /> 
                    <div>Profile</div>
                </Button>
              </Link>

              <div className="flex flex-col gap-3 mt-[40vh]">
                <Button variant='ghost' className='justify-start gap-3 px-2' onClick={handleToggle}>
                  {theme === 'dark' ? <SunMoon />:  <MoonStar />}
                  <div>{theme === 'dark' ? 'Light Mode':  'Dark Mode'}</div>
                </Button>
                <Button variant='ghost' className='justify-start gap-3 px-2'>
                  <MdLogout size={25} />
                  <div>Disconnect</div>
                </Button>
              </div>

            </SheetContent>
          </Sheet>

    
    </div>
  )
}

export default SideBar