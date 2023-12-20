import React from 'react'
import { TypographyH2, TypographyH3, TypographyH4 } from '../common/Typography'
import { Card } from '../ui/card'
import { Avatar } from '../common/Avatar'

type Props = {}

const TopCreators = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
        <TypographyH2 text='Top Creators'/>
        <TypographyH4 text='Checkout Top Rated Creators on the NFT Marketplace'/>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
            {
                Array.from({length:8}).map((_, index) => 
                    <Creator key={index}/>
                )
            }

        </div>

    </div>
  )
}


const Creator = (props: Props) => {
  return (
    <Card className='flex flex-col justify-evenly items-center p-5 h-[16rem] bg-secondary'>
        <Avatar className='h-20 w-20'/>
        <TypographyH3 text='Bruce Banner'/>
        <div className='flex items-center gap-3'>
            <TypographyH4 className='text-primary/80' text='Total Sales: '/>
            <TypographyH4 text='34.5ETH'/>
        </div>
    </Card>
  )
}

export default TopCreators