'use client'
import Logo from '../Logo'
import Profile from './Profile'
import Notifications from './Notifications'
import { SunMoon, MoonStar } from 'lucide-react';
import { useTheme } from "next-themes"
 
import { Button } from "@/components/ui/button"
import Container from '../Container'
import SideBar from './SideBar'
import Link from 'next/link';
import Discover from './Discover';
import HelpCenter from './HelpCenter';

type Props = {}

const NavBar = (props: Props) => {
  const { theme, setTheme } = useTheme()
  const handleToggle = () => {
    if(theme === 'light'){
      setTheme('dark')
    }else{
      setTheme('light')
    }
  }
  return (
    <div className='fixed top-0 w-full z-50'>
      <Container className='flex items-center justify-between border-b'>
          {/* Left Section */}
          <Link href='/' className='flex gap-2 items-center'>
              <Logo/>
              <div className='text-lg lg:text-2xl font-bold'>NFT Marketplace</div>
          </Link>

          {/* Right Section */}
          <div className='hidden lg:flex gap-5 lg:gap-12 items-center'>
            {/* Discover Section */}
            <Discover/>

            {/* Help Center Section */}
            <HelpCenter/>

            {/* Notification Section */}
            <Notifications/>

            {/* Toggle Theme  */}
            <Button variant='ghost' className='rounded-full' onClick={handleToggle}>
              {theme === 'dark' ? <SunMoon />:  <MoonStar />}
            </Button>

            {/* Create NFT Section*/}
            <Button className='rounded-full' size='lg'>Create</Button>

            {/* User Profile Section */}
            <Profile/>
          </div>

          {/* Mobile Sidebar */}
          <SideBar />
      </Container>
    </div>
  )
}


export default NavBar