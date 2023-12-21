'use client'
import Image from 'next/image'
import { Card } from './ui/card'
import { TypographyH3, TypographyH4, TypographySmall } from './common/Typography'
import { Button } from './ui/button'
import { FaHeart } from "react-icons/fa";
import { Badge } from './ui/badge';
import CountDown from 'count-down-react'

type Props = {}

const NFTCard = (props: Props) => {

const onTick = ({ hours, minutes, seconds }:{hours:number, minutes:number, seconds:number }) => {

    return <>{`${hours}h:${minutes}m:${seconds}s`}</> 
  };
  
  return (
    <Card className='self-start p-5 bg-accent max-w-[35rem] w-full'>
        <div className='relative overflow-clip  h-[30rem]'>
            <Image className='object-cover rounded-lg' src='/landing-page/audio-category.jpg' fill alt=''/>
            <div className='absolute rounded-bl-[0.5rem] transform skew-x-[45deg] -top-0 right-0 py-[0.5rem] w-[60%] mr-[-3rem] bg-accent'>
                <div className='text-center transform skew-x-[-45deg]'>
                     <TypographySmall className='text-primary/60' text='Remaining Time'/>
                     <TypographyH3 
                        className='text-primary/60' 
                        text={        
                        <CountDown
                            date={Date.now() + 50000000}
                            renderer={onTick}
                        />}/>
                </div>
            </div>
            <div className='absolute rounded-tr-[0.5rem] transform skew-x-[50deg] bottom-0 left-0 py-[0.5rem] w-[80%] ml-[-4.5rem] bg-accent'>
                <div className='flex flex-col items-start pl-[4.5rem] gap-5 transform skew-x-[-50deg]'>
                    <TypographyH3 className='text-primary/60' text='Clone #12232'/>
                    <Card className='relative p-3 bg-primary/5'>
                        <Badge className='absolute -top-3 left-1'>Current Bid</Badge>
                        <TypographyH4 className='text-primary/60' text='0.001245ETH'/>
                    </Card>
                </div>
            </div>
            <Badge className='flex items-center gap-3 absolute top-5 left-5 ' >
                <Button variant='ghost' size={'sm'} className='rounded-full h-auto p-2'>
                    <FaHeart className='text-red-500 p-0' size={20} />
                </Button>
                <TypographySmall text='22'/>
            </Badge>
        </div>
    </Card>
  )
}

export default NFTCard