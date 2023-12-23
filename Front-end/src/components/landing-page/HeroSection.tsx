import Image from 'next/image'
import { Button } from '../ui/button'
import Link from 'next/link'

type Props = {}

const HeroSection = (props: Props) => {
  return (
    <div className='flex flex-col-reverse lg:flex-row justify-center items-center min-h-[70vh] h-full'>
        <div className='flex-1 flex flex-col gap-5 p-2 lg:p-8'>
            <div className='text-4xl font-extrabold tracking-tight lg:text-7xl'>Discover Digital Products & Collect as NFTs</div>
            <div className='text-3xl'>Trade unique digital products as NFTs on NFT Marketplace, the innovative platform empowering creators and collectors.</div>
            <Link href='/sign-up'>
                <Button className='self-start' size={'lg'}>Get Started</Button>
            </Link>
        </div>
        <div className='flex-1'>
            <Image className='object-cover' src='/landing-page/hero-image.png' width={1000} height={501} alt=''/>
        </div>
        
    </div>
  )
}

export default HeroSection