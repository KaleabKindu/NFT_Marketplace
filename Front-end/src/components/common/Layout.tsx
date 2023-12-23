import React, { ReactNode } from 'react'
import NavBar from './NavBar'
import Container from './Container'
import Footer from './Footer'

type Props = {
    children:ReactNode
}

const Layout = ({ children }: Props) => {
  return (
    <>
        <NavBar/>
        <div className='pt-[5rem]'>{children}</div>
        <Footer/>
    </>
  )
}

export default Layout