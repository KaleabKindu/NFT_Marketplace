'use client'
import Logo from '../Logo'
import Profile from './Profile'
import Notifications from './Notifications'
import { SunMoon, MoonStar } from 'lucide-react';
 
import { Button } from "@/components/ui/button"
import Container from '../Container'
import SideBar from './SideBar'
import Discover from './Discover';
import HelpCenter from './HelpCenter';
import Link from 'next/link';
import { Routes } from '@/routes';
import { useWeb3Modal } from '@web3modal/wagmi/react';
import useWeb3Status from '@/hooks/useWeb3Status';
import useWebTheme from '@/hooks/useWebTheme';
type Props = {}

const NavBar = (props: Props) => {
  const { mode, handleToggle } = useWebTheme()
  const { connected } = useWeb3Status()
  const { open } = useWeb3Modal()
  return (
    <div className='fixed top-0 w-full bg-background  z-50'>
      <Container className='flex items-center justify-between border-b'>
          {/* Left Section */}
          <Logo/>

          {/* Right Section */}
          <div className='hidden lg:flex gap-5 items-center'>
            {/* Discover Section */}
            <Discover/>

            {/* Help Center Section */}
            <HelpCenter/>

            {/* Notification Section */}
            {connected && <Notifications/>}

            {/* Toggle Theme  */}
            <Button variant='ghost' size={'icon'} className='rounded-full' onClick={handleToggle}>
              {mode === 'dark' ? <SunMoon />:  <MoonStar />} 
            </Button>

            {connected ? 
            <Link href={Routes.MINT}>
              <Button className='rounded-full' size='lg'>Create</Button>
            </Link>:
              <Button className='rounded-md' onClick={() => open()}>Connect Wallet</Button>
            }

            {/* User Profile Section */}
            {connected && <Profile/>}
          </div>

          {/* Mobile Sidebar */}
          <SideBar />
      </Container>
    </div>
  )
}


export default NavBar