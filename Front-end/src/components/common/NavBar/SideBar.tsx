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
import { Loader2, MoonStar, SunMoon } from "lucide-react";
import useWebTheme from "@/hooks/useWebTheme";
import { useAccount, useDisconnect, useSignMessage } from "wagmi";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import { useAuthenticateSignatureMutation, useGetNounceMutation } from "@/store/api";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { useToast } from "@/components/ui/use-toast";
import { useState } from "react";
import { Address } from "@/types";
import { ToastAction } from "@/components/ui/toast";
import { setSession } from "@/store/slice/auth";
import { persistor } from "@/store";

type Props = {

}

const SideBar = (props: Props) => {
  const dispatch = useAppDispatch()
  const session = useAppSelector(state => state.auth.session)
  const { mode, handleToggle } = useWebTheme()
  const { toast } = useToast()
  const { address, isConnected } = useAccount({ 
    onConnect:() => {
    if(!session){
      signIn()
    }
  }})
  const { disconnect } = useDisconnect()
  const [getNonce] = useGetNounceMutation()
  const [authenticateSignature] = useAuthenticateSignatureMutation()
  const [ loading, setLoading ] = useState(false)
  const { signMessageAsync } = useSignMessage()
  const { open } = useWeb3Modal()
  const handleLogin = () => {
    if(isConnected){
      signIn()
    }else{
      open()
    }
  }

  const signIn = async () => {
    try {
      setLoading(true)
      const nonce = await getNonce(address as Address).unwrap()

      const signature = await signMessageAsync({ message:nonce })
      console.log(signature)
      const token = await authenticateSignature({ publicAddress:address as Address, signedNonce:signature}).unwrap()
      console.log('session', token)
      dispatch(setSession(token))
    } catch (error) {
      console.log(error)
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        action: <ToastAction altText='Try Again'>Try Again</ToastAction>
      })
    } finally{
      setLoading(false)
    }
  }
  const handleLogout = () => {
    disconnect()
    persistor.purge()
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
            {session && 
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
          {session && 
          <Link href='' className="flex  w-full">
            <Button variant='ghost' className='w-full gap-3 justify-start items-center px-3'>
                <FaUserAlt size={25} /> 
                <div>Profile</div>
            </Button>
          </Link>}
          {!session &&
          <Button disabled={loading} className='w-full gap-3 justify-center mt-8 items-center px-3' onClick={() => handleLogin()}>
            { loading ? 
              <>
                <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                Logging In
              </>: 'Connect'}
          </Button>}

          <div className="flex flex-col gap-3 mt-[40vh]">
            <Button variant='ghost' className='justify-start gap-3 px-2' onClick={handleToggle}>
              {mode === 'dark' ? <SunMoon />:  <MoonStar />}
              <div>{mode === 'dark' ? 'Light Mode':  'Dark Mode'}</div>
            </Button>
            {session && 
            <Button variant='ghost' className='justify-start gap-3 px-2' onClick={() => handleLogout()}>
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