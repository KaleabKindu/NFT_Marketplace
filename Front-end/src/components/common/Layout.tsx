import React, { ReactNode } from 'react'
import NavBar from './NavBar'
import Container from './Container'

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
    </>
  )
}

export default Layout