import { discover, help_center } from '@/data'
import React from 'react'
import Logo from './Logo'
import Container from './Container'
import Link from 'next/link'
import { FaFacebook, FaLinkedin, FaYoutube } from 'react-icons/fa'
import { FaXTwitter } from "react-icons/fa6";
import { Button } from '../ui/button'
import { Input } from '../ui/input'
type Props = {}

const Footer = (props: Props) => {
  return (
    <Container>
        <div className='grid grid-cols-1 lg:grid-cols-4 gap-5 py-5 lg:py-10 border-t'>
            {/* First Section */}
            <div className='flex flex-col justify-between max-w-[70%] lg:max-w-full mx-auto'>
                <Logo/>
                <div className='flex flex-col gap-5'>
                    <div className='p-3'>The Largest NFT Marketplace for Digital Products. Buy, Sell and Discover Exclusive Digital Products as Non-Fungible Token(NFTs) </div>
                    <div className='flex gap-5 items-center justify-center lg:justify-start'>
                        <Button variant='ghost' className='rounded-full'>
                            <Link href=''>
                                <FaFacebook size={25}/>
                            </Link>
                        </Button>
                        <Button variant='ghost' className='rounded-full'>
                            <Link href=''>
                                <FaLinkedin size={25}/>
                            </Link>
                        </Button>
                        <Button variant='ghost' className='rounded-full'>
                            <Link href=''>
                                <FaXTwitter size={25}/>
                            </Link>
                        </Button>
                        <Button variant='ghost' className='rounded-full'>
                            <Link href=''>
                                <FaYoutube size={25}/>
                            </Link>

                        </Button>                    </div>
                </div>
            </div>

            {/* Second Section */}
            <div className='flex flex-col gap-5 items-center p-2'>
                <div className='text-xl font-semibold'>Discover</div>
                {
                    discover.map((option, index) => 
                    <Link className='w-[40%] text-center rounded-md py-1 hover:bg-accent' href='/' key={index}>{option.name}</Link>
                    )
                }
            </div>

            {/* Third Section */}
            <div className='flex flex-col gap-5 items-center p-2'>
                <div className='text-xl font-semibold'>Help Center</div>
                {
                    help_center.map((option, index) => 
                    <Link className='w-[40%] text-center rounded-md py-1 hover:bg-accent' href='/' key={index}>{option.name}</Link>
                    )
                }
            </div>

            {/* Fourth Section */}
            <div className='flex flex-col gap-5 justify-around items-center p-2'>
                <div className='text-xl font-semibold'>Subscribe</div>
                <div>Get exclusive promotions & updates straight to your inbox.</div>
                <div className='flex gap-3 items-center'>
                    <Input type="email" className='bg-accent text-accent-foreground rounded-full px-6' placeholder="Enter your Email Address" />
                    <Button className='rounded-full'>Subscribe</Button>
                </div>
            </div>

            
        </div>
    </Container>
  )
}

export default Footer