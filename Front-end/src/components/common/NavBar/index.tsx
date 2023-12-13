'use client'
import Logo from '../Logo'
import Profile from './Profile'
import Notifications from './Notifications'
import { SunMoon, MoonStar } from 'lucide-react';
import { useTheme } from "next-themes"
 
import { Button } from "@/components/ui/button"
import Container from '../Container'
import SideBar from './SideBar'

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
          <div className='flex gap-2 items-center'>
              <Logo/>
              <div className='text-lg lg:text-2xl font-bold'>NFT Marketplace</div>
          </div>
          <div className='hidden lg:flex gap-5 lg:gap-16 items-center'>
            <Button variant='ghost' className='rounded-full' size='lg'>Marketplace</Button>
            <Button className='rounded-full' size='lg'>Create</Button>
            <Button variant='ghost' className='rounded-full' onClick={handleToggle}>
              {theme === 'dark' ? <SunMoon />:  <MoonStar />}
            </Button>
            <Notifications/>
            <Profile/>
          </div>
          <SideBar />
      </Container>
    </div>
  )
}


export default NavBar