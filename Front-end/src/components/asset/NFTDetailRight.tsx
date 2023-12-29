'use client'
import { TypographyH2, TypographyH3, TypographyH4, TypographyP, TypographySmall } from '../common/Typography'
import Link from 'next/link'
import CountDown from 'count-down-react'
import { Button } from '../ui/button'
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
  } from "@/components/ui/accordion"

type Props = {}

const NFTDetailRight = (props: Props) => {
    const onRender = ({ days, hours, minutes, seconds }:{days:number, hours:number, minutes:number, seconds:number }) => {
        return (
        <div className='grid grid-cols-4 gap-2 lg:w-[70%]'>
            <div>{days}</div>
            <div>{hours}</div>
            <div>{minutes}</div>
            <div>{seconds}</div>
            <div>Days</div>
            <div>Hours</div>
            <div>Minutes</div>
            <div>Seconds</div>
        </div>)
        };
  return (
    <div className='flex-1 p-3'>
        <div className='flex flex-col gap-10'>
            <TypographyH2 text='Boomerang'/>
            <div className='flex flex-wrap items-center lg:divide-x-2'>
              <Link href={`${Routes.USER}`} className='flex items-center gap-3 p-5'>
                <Avatar className='h-12 w-12'/>
                <div className='flex flex-col'>
                  <TypographySmall text='Creator'/>
                  <TypographyH4 text='Anthony Stark'/>
                </div>
              </Link>
              <Link href={`${Routes.USER}`} className='flex items-center gap-3 p-5'>
                <Avatar className='h-12 w-12'/>
                <div className='flex flex-col'>
                  <TypographySmall text='Owner'/>
                  <TypographyH4 text='Bruce Banner'/>
                </div>
              </Link>
              <Link href={`${Routes.COLLECTION}`} className='flex items-center gap-3 p-5'>
                <Avatar className='h-12 w-12' src='/collection/collection.png'/>
                <div className='flex flex-col'>
                  <TypographySmall text='Collection'/>
                  <TypographyH4 text='Avengers'/>
                </div>
              </Link>
            </div>
            <div className='flex flex-col gap-5 border rounded-md bg-secondary/50'>
                <div className='flex flex-col gap-5 border-b p-5'>
                    <TypographyH2 text='Auction Ends in:'/>
                    <TypographyH3 
                            className='text-primary/60' 
                            text={        
                            <CountDown
                                date={Date.now() + 5000000000}
                                renderer={onRender}
                            />}/>
                </div>
                <div className='flex flex-col gap-10 p-5'>
                    <div>
                        <TypographyP text='Current Price'/>
                        <div className='flex gap-2 items-end'>
                            <TypographyH2  text={`${'0.3948'} ETH`}/>
                            <TypographyP className='text-primary/60' text={`$${807.07}`}/>
                        </div>
                    </div>
                    <div className='flex gap-5'>
                        <Button className='flex-1' >Place Bid</Button>
                        <Button className='flex-1' variant={'secondary'}>Make Offer</Button>
                    </div>
                </div>
            </div>
            <Accordion type="single" collapsible defaultValue='item-1'>
                <AccordionItem value="item-1">
                    <AccordionTrigger className="bg-accent text-accent-foreground px-5 mb-5 rounded-t-md">Offers</AccordionTrigger>
                    <AccordionContent className=""><OffersTable/></AccordionContent>
                </AccordionItem>
                <AccordionItem value="item-2">
                    <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">Bids</AccordionTrigger>
                    <AccordionContent className=""><BidsTable/></AccordionContent>
                </AccordionItem>
            </Accordion>
        </div>
    </div>
  ) 
}

export default NFTDetailRight



import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
  } from "@/components/ui/table"
import { bids, offers } from '@/data'
import { Avatar } from '../common/Avatar'
import { Routes } from '@/routes'
  
  
  export function OffersTable() {
    return (
      <Table className='border'>
        <TableHeader>
          <TableRow>
            <TableHead>Price</TableHead>
            <TableHead>USD Price</TableHead>
            <TableHead>From</TableHead>
            <TableHead>Date</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {offers.map((offer, index) => (
            <TableRow key={index}>
              <TableCell className="font-medium">{`${offer.price} WETH`}</TableCell>
              <TableCell>{`$${offer.usd_price}`}</TableCell>
              <TableCell>
                <Link href={''}>
                    {offer.from}
                </Link>
              </TableCell>
              <TableCell>{offer.date}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    )
  }



  export function BidsTable() {
    return (
      <Table className='border'>
        <TableHeader>
          <TableRow>
            <TableHead>Bid</TableHead>
            <TableHead>Bid in USD</TableHead>
            <TableHead>From</TableHead>
            <TableHead>Date</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {bids.map((bid, index) => (
            <TableRow key={index}>
              <TableCell className="font-medium">{`${bid.bid_price} WETH`}</TableCell>
              <TableCell>{`$${bid.bid_usd_price}`}</TableCell>
              <TableCell>
                <Link href={''}>
                    {bid.from}
                </Link>
              </TableCell>
              <TableCell>{bid.date}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    )
  }
  