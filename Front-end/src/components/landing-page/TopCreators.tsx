'use client'
import Image from 'next/image'
import { TypographyH2, TypographyH4, TypographyP } from '../common/Typography'
import { Card } from '../ui/card'
import { Avatar } from '../common/Avatar'
import { Button } from '../ui/button'
import { Badge } from '../ui/badge'
import { useState } from 'react'

type Props = {}

const TopCreators = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
        <TypographyH2 text='Top Creators'/>
        <TypographyH4 text='Checkout Top Rated Creators on the NFT Marketplace'/>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
            {
                Array.from({length:8}).map((_, index) => 
                    <Creator key={index} index={index}/>
                )
            }

        </div>

    </div>
  )
}

type CreatorProps = {
  index:number
}

export const Creator = ({index}: CreatorProps) => {
  const [ following, setFollowing ] = useState(false)

  return (
    <Card className='relative flex flex-col justify-evenly items-center h-[20rem] bg-secondary group'>
        <div className='relative overflow-clip w-full h-[55%] '>
          <Image className='object-cover rounded-t-lg group-hover:scale-105' src='/landing-page/futuristic-blue.jpg' fill alt=''/>
        </div>
        <div className='relative z-40 rounded-full bg-secondary -mt-12 p-4'>
          <Avatar className='h-20 w-20'/>
        </div>
        <div className='flex gap-3 items-center justify-around w-full p-3'>
          <div>
            <TypographyH4 text='Bruce Banner'/>
            <div className='flex items-center gap-3'>
                <TypographyP className='font-semibold text-primary/80' text='Sales: '/>
                <TypographyP className='font-semibold' text='34.5ETH'/>
            </div>
          </div>
          <Button className='text-md rounded-full' onClick={() => setFollowing(!following)}>{following ? 'Unfollow':'Follow'}</Button>
        </div>
        <Badge className='absolute top-5 left-5 text-md bg-accent text-accent-foreground'>{index + 1}</Badge>
    </Card>
  )
}

export default TopCreators