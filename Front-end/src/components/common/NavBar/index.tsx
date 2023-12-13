'use client'
import Logo from '../Logo'
import { Button, Typography } from '@material-tailwind/react'
import Profile from './Profile'
import Notifications from './Notifications'
import { MdDarkMode, MdLightMode } from "react-icons/md";
import { useEffect, useState } from 'react'
import Container from '../Container'
import SideBar from './SideBar'

type Props = {}

const NavBar = (props: Props) => {
  const [ darkMode, setDarkMode ] = useState(false)
  const handleToggle = () => {
    if(localStorage.getItem('theme') === 'dark'){
      document.documentElement.classList.remove('dark');
      setDarkMode(false)
    }else{
      document.documentElement.classList.add('dark');
      setDarkMode(true)
    }
  }
  useEffect(() => {
    const theme = localStorage.getItem('theme')
    if(theme){
      setDarkMode(true ? theme === 'dark':theme === 'light')
    }else{
      setDarkMode(false)
      localStorage.setItem('theme', 'light')

    }
  },[])
  return (
    <div className='fixed top-0 w-full z-50'>
      <Container className='flex items-center justify-between dark:bg-dark-bg border-b dark:border-b-dark-border'>
          <div className='flex gap-2 items-center'>
              <Logo/>
              <Typography className='dark:text-dark-text text-lg lg:text-2xl' variant='h3'>NFT Marketplace</Typography>
          </div>
          <div className='hidden lg:flex gap-5 lg:gap-16 items-center'>
            <Button className='rounded-full dark:text-dark-text dark:bg-dark-bg dark:hover:bg-dark-hover-bg' variant='text' size='lg'>Marketplace</Button>
            <Button className='rounded-full bg-primary' size='lg'>Create</Button>
            <Button variant='text' className='rounded-full p-3 dark:hover:bg-dark-hover-bg' onClick={handleToggle}>
              {darkMode ? <MdLightMode className=' dark:text-dark-text' size={30}/>:  <MdDarkMode className=' dark:text-dark-text' size={30}/>}
            </Button>
            <Notifications/>
            <Profile/>
          </div>
          <SideBar darkMode={darkMode} handleToggle={handleToggle}/>
      </Container>
    </div>
  )
}

export default NavBar