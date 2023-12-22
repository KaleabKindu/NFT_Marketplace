'use client'
import { TbFilterSearch } from "react-icons/tb";
import { IoChevronDown } from "react-icons/io5";
import { Button } from '../ui/button'
import { CategoryFilter, PriceFilter, SaleFilter, SortFilter } from '../collection/SearchFilter'
import { useState } from 'react'
import { cn } from "@/lib/utils";

type Props = {}

const Filter = (props: Props) => {
  const [ showFilter, setShowFilter ] = useState(false)
  const [ selected, setSelected ] = useState('all')
  return (
    <div className='flex flex-col gap-5 w-full'>
      <div className='flex items-center justify-between border-b py-6'>
        <div className='flex gap-5'>
          <Button variant={selected === 'all' ? 'default':'ghost'} className='rounded-full' onClick={() => setSelected('all')}>All</Button>
          <Button variant={selected === 'nft' ? 'default':'ghost'} className='rounded-full' onClick={() => setSelected('nft')}>NFT</Button>
          <Button variant={selected === 'collection' ? 'default':'ghost'} className='rounded-full' onClick={() => setSelected('collection')}>Collections</Button>
        </div>
        <Button className="rounded-full" onClick={() => setShowFilter(!showFilter)}>
          <TbFilterSearch className='mr-2' size={25}/>
          Filter
          <IoChevronDown className={cn('ml-2', { "transform rotate-180":showFilter})} size={25}/>
        </Button>
      </div>
      {showFilter &&
      <div className="flex flex-wrap items-center gap-5">
          <PriceFilter/>
          <SaleFilter/>
          <CategoryFilter/>
          <SortFilter/>
      </div>}
      
    </div>
  )
}

export default Filter