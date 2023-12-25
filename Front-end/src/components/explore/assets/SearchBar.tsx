import React from 'react'
import { CiSearch } from 'react-icons/ci'
import { Input } from '../../ui/input'
import { cn } from '@/lib/utils'
import { Button } from '../../ui/button'
import { MdArrowForward } from 'react-icons/md'
type Props = {
  className?:string
}

const SearchBar = ({className}: Props) => {
  return (
    <div className={cn("relative", className)}>
        <CiSearch className="absolute top-0 bottom-0 my-auto left-3" size={25}/>
        <Input type="text" placeholder="Search" className="rounded-full h-12 pl-12 pr-12 bg-accent text-accent-foreground focus:border-background/80" />
        <Button className="absolute top-0 bottom-0 my-auto right-2 rounded-full" size={'icon'}>
          <MdArrowForward size={30}/>
        </Button>
    </div>
  )
}

export default SearchBar