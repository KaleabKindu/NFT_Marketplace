import Image from 'next/image'

type Props = {}

const Logo = (props: Props) => {
  return (
    <Image className='w-[80px] h-[60px] lg:w-[100px] lg:h-[70px]' src='/logo.png' width={150} height={150} alt="NFT Marketplace's Logo"/>
  )
}

export default Logo