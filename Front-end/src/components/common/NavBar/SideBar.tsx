'use client'
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet"
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger
} from "@/components/ui/accordion"


import { RiMenu3Fill } from "react-icons/ri";
import { FaUserAlt } from "react-icons/fa";
import { MdLogout } from "react-icons/md";
import { Button } from '@/components/ui/button';

import Link from "next/link";

import Logo from "../Logo";
import { discover, help_center } from "@/data";
import { MoonStar, SunMoon } from "lucide-react";
import useWeb3Status from "@/hooks/useWeb3Status";
import useWebTheme from "@/hooks/useWebTheme";
import { useDisconnect } from "wagmi";
import { useWeb3Modal } from "@web3modal/wagmi/react";

type Props = {

}

const SideBar = (props: Props) => {
  const { mode, handleToggle } = useWebTheme()
  const { open } = useWeb3Modal()
  const { connected } = useWeb3Status()
  const { disconnect } = useDisconnect()  
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
            {connected && 
            <AccordionItem value="item-3" className="border-b mb-[2.5rem]">
              <AccordionTrigger>Notifications</AccordionTrigger>
              <AccordionContent>
                <div className="p-3">
                  <div> Measure action your user...</div>
                  <div className="text-sm font-medium leading-none">4 minutes ago</div>
                </div>
              </AccordionContent>
            </AccordionItem>}
          </Accordion>
          {connected && 
          <Link href='' className="flex  w-full">
            <Button variant='ghost' className='w-full gap-3 justify-start items-center px-3'>
                <FaUserAlt size={25} /> 
                <div>Profile</div>
            </Button>
          </Link>}
          {!connected &&
          <Button  className='w-full gap-3 justify-center mt-8 items-center px-3' onClick={() => open()}>
            Connect Wallet
          </Button>}

          <div className="flex flex-col gap-3 mt-[40vh]">
            <Button variant='ghost' className='justify-start gap-3 px-2' onClick={handleToggle}>
              {mode === 'dark' ? <SunMoon />:  <MoonStar />}
              <div>{mode === 'dark' ? 'Light Mode':  'Dark Mode'}</div>
            </Button>
            {connected && 
            <Button variant='ghost' className='justify-start gap-3 px-2' onClick={() => disconnect()}>
              <MdLogout size={25} />
              <div>Disconnect</div>
            </Button>}
          </div>

        </SheetContent>
      </Sheet>
    </div>
  )
}

export default SideBar