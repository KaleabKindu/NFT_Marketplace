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
        <Container className='pt-[6.5rem]'>
            {children}
        </Container>
        <Footer/>
    </>
  )
}

export default Layout