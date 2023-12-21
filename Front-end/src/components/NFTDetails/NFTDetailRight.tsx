'use client'
import { TypographyH2, TypographyH3, TypographyP } from '../common/Typography'
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
            <div>
                <TypographyH2 text='Boomerang'/>
                <div className='flex gap-2 items-center'>
                    <TypographyP text='Owned By'/>
                    <Link href={''}>
                        <span className='text-blue-500'> A60E90</span>
                    </Link>
                </div>
            </div>
            <div className='flex gap-5 items-center'>
                <div>Art</div>
                <div>Collection</div>
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
                    <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">Listings</AccordionTrigger>
                    <AccordionContent className=""><ListingsTable/></AccordionContent>
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
import { offers } from '@/data'
  
  
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



  export function ListingsTable() {
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
  