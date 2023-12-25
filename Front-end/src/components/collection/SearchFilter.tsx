'use client'
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
import { Badge } from "../ui/badge"
import { IoWalletOutline, IoChevronDown } from "react-icons/io5";
import { TypographyP, TypographySmall } from "../common/Typography";
import { Button } from "../ui/button";
import { IoMdCloseCircle } from "react-icons/io";
import { Slider } from "@/components/ui/slider"
import { BiSortAlt2, BiCategory } from "react-icons/bi";
import { categories, collections, sale_types, sort_types, users } from "@/data";
import { Checkbox } from "../ui/checkbox";
import { Label } from "@/components/ui/label"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import { useState } from "react";
import { MdOutlineSell } from "react-icons/md";
import { cn } from "@/lib/utils";
import { CiSearch } from "react-icons/ci";
import { Input } from "../ui/input";
import { Check, ChevronsUpDown } from "lucide-react"
import { MdOutlineCollectionsBookmark } from "react-icons/md";
import { LuUser2 } from "react-icons/lu";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command"
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { Avatar } from "../common/Avatar";
type Props = {}

const SearchFilter = (props: Props) => {
  return (
    <div className="flex flex-col lg:flex-row gap-8 items-center mt-16">
        <SearchInput className='flex-1' />
        <div className="flex-1 flex flex-wrap justify-center items-center gap-5">
            <PriceFilter/>
            <SaleFilter/>
            <CategoryFilter/>
            <SortFilter/>
        </div>
    </div>
  )
}

export default SearchFilter

type SearchProps = {
    className?:string
}

const SearchInput = ({className}: SearchProps) => {
  return (
    <div className={cn("relative", className)}>
        <CiSearch className="absolute top-0 bottom-0 my-auto left-3" size={25}/>
        <Input type="text" placeholder="Search" className="rounded-full pl-12 pr-4 bg-accent text-accent-foreground focus:border-background/80" />
    </div>

  )
}



export const SaleFilter = (props: Props) => {
    const [ selectedSaleTypes, setSelectedSaleType ] = useState<string[]>([])
    const handleChange = (status:boolean, value:string) => {
        if(status){
            setSelectedSaleType([...selectedSaleTypes, value])
        }else{
            setSelectedSaleType(selectedSaleTypes.filter(val => val !== value))
        }
    }
  return (
    <DropdownMenu>
        <DropdownMenuTrigger>
            <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
                <MdOutlineSell size={25}/>
                <TypographySmall className='text-foreground' text='Sale Type'/>
                <Button variant='ghost' size='sm' className="p-0 h-auto rounded-full" onClick={() => setSelectedSaleType([])}>
                    {selectedSaleTypes.length > 0 ? <IoMdCloseCircle size={25} />:<IoChevronDown size={20}/>}
                </Button>
            </Badge>
        </DropdownMenuTrigger>
        <DropdownMenuContent className="rounded-xl">
            <div className="flex flex-col gap-3 w-[20rem] p-5">
                <div className="flex gap-3 items-center">
                    <Checkbox checked={selectedSaleTypes.includes('all')} onCheckedChange={(val:boolean) => handleChange(val, 'all')} />
                    <TypographyP text='All'/>
                </div>
            {
                sale_types.map((sale, index) => 
                <div key={index} className="flex gap-3 items-center">
                    <Checkbox checked={selectedSaleTypes.includes(sale.name)} onCheckedChange={(val:boolean) => handleChange(val, sale.name)} />
                    <TypographyP text={sale.name}/>
                </div>
                )
            }
            </div>
            <DropdownMenuSeparator/>
            <div className="flex justify-between items-center p-5">
                <Button className="rounded-full" onClick={() => setSelectedSaleType([])} variant='outline'>Clear</Button>
                <Button className="rounded-full">Apply</Button>
            </div>
        </DropdownMenuContent>
    </DropdownMenu>
  )
}

export const PriceFilter = (props: Props) => {
    return (
      <DropdownMenu>
          <DropdownMenuTrigger>
            <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
                <IoWalletOutline size={25}/>
                <TypographySmall className='text-foreground' text='0.01ETH - 10ETH'/>
                <Button variant='ghost' size='sm' className="p-0 h-auto rounded-full">
                    <IoMdCloseCircle size={25} />
                </Button>
            </Badge>
          </DropdownMenuTrigger>
          <DropdownMenuContent className="rounded-xl">
            <div className="flex flex-col gap-5 w-[20rem] p-5">
                <TypographyP text='Price Range'/>
                <Slider defaultValue={[33]} max={100} step={1} />
                <div className="flex justify-between">
                    <div className="flex flex-col gap-1.5 w-[40%]">
                        <TypographySmall className="ml-2" text='Min Price'/>
                        <div className="flex justify-between items-center rounded-xl border p-3">
                            <TypographySmall text='0.01'/>
                            <TypographySmall text='ETH'/>
                        </div>
                    </div>
                    <div className="flex flex-col gap-1.5 w-[40%]">
                        <TypographySmall className="mr-2 self-end" text='Max Price'/>
                        <div className="flex justify-between items-center rounded-xl border p-3">
                            <TypographySmall text='10'/>
                            <TypographySmall text='ETH'/>
                        </div>
                    </div>
                </div>
            </div>
            <DropdownMenuSeparator/>
            <div className="flex justify-between items-center p-5">
                <Button className="rounded-full" onClick={() => {}} variant='outline'>Clear</Button>
                <Button className="rounded-full">Apply</Button>
            </div>
          </DropdownMenuContent>
      </DropdownMenu>
    )
  }

export const CategoryFilter = (props: Props) => {
    const [ selectedCategories, setSelectedCategories ] = useState<string[]>([])
    const handleChange = (status:boolean, value:string) => {
        if(status){
            setSelectedCategories([...selectedCategories, value])
        }else{
            setSelectedCategories(selectedCategories.filter(val => val !== value))
        }
    }
return (
    <DropdownMenu>
        <DropdownMenuTrigger>
            <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
                <BiCategory size={25}/>
                <TypographySmall className='text-foreground' text='Category'/>
                <Button variant='ghost' size='sm' className="p-0 h-auto rounded-full" onClick={() => setSelectedCategories([])}>
                    {selectedCategories.length > 0 ? <IoMdCloseCircle size={25} />:<IoChevronDown size={20}/>}
                </Button>
            </Badge>
        </DropdownMenuTrigger>
        <DropdownMenuContent className="rounded-xl">
            <div className="flex flex-col gap-3 w-[20rem] p-5">
                <div className="flex gap-3 items-center">
                    <Checkbox checked={selectedCategories.includes('all')} onCheckedChange={(val:boolean) => handleChange(val, 'all')} />
                    <TypographyP text='All'/>
                </div>
            {
                categories.map((category, index) => 
                <div key={index} className="flex gap-3 items-center">
                    <Checkbox checked={selectedCategories.includes(category.name)} onCheckedChange={(val:boolean) => handleChange(val, category.name)} />
                    <TypographyP text={category.name}/>
                </div>
                )
            }
            </div>
            <DropdownMenuSeparator/>
            <div className="flex justify-between items-center p-5">
                <Button className="rounded-full" onClick={() => setSelectedCategories([])} variant='outline'>Clear</Button>
                <Button className="rounded-full">Apply</Button>
            </div>
        </DropdownMenuContent>
</DropdownMenu>
)
}

export const SortFilter = (props: Props) => {
const [ open, setOpen ] = useState(false)
const [ sortBy, setSortBy ] = useState('')
const handleChange = (e:string) => {
    setSortBy(e)
}

return (
    <DropdownMenu open={open} onOpenChange={(val) => setOpen(val)}>
    <DropdownMenuTrigger>
        <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
            <BiSortAlt2 size={25}/>
            <TypographySmall className='text-foreground' text={sortBy ? sortBy:'Sort By'}/>
            <Button 
             variant='ghost'
             size='sm'
             className="p-0 h-auto rounded-full" 
             onClick={(e) => {
                console.log('here')
                if(sortBy){
                    e.stopPropagation()
                    setSortBy('')
                }
             }}>
                {sortBy ? <IoMdCloseCircle size={25} />:<IoChevronDown size={20}/>}
            </Button>
        </Badge>
    </DropdownMenuTrigger>
    <DropdownMenuContent className="rounded-xl">
        <RadioGroup className="p-5 w-[20rem]" value={sortBy} onValueChange={(e) => handleChange(e)} defaultValue="option-one">
            {
                sort_types.map((type, index) => 
                <div key={index} className="flex items-center space-x-2">
                    <RadioGroupItem className="w-5 h-5" value={type.value} id={type.value} />
                    <Label className="text-md" htmlFor={type.value}>{type.name}</Label>
                </div>
                )
            }
        </RadioGroup>
        <DropdownMenuSeparator/>
        <div className="flex justify-between items-center p-5">
            <Button className="rounded-full" variant='outline' size='sm' onClick={() => setSortBy('')}>Clear</Button>
            <Button className="rounded-full" size='sm'>Apply</Button>
        </div>
    </DropdownMenuContent>
</DropdownMenu>
)
}
 

export const CollectionsFilter = (props: Props) => {
    const [open, setOpen] = useState(false)
    const [value, setValue] = useState("")
    return (
        <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger >
        <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
        <MdOutlineCollectionsBookmark size={25}/>
        {value
              ? collections.find((collection) => collection.name.toLowerCase() === value)?.name
              :<TypographySmall className='text-foreground' text='Collection'/>
            }
            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
        </Badge>
        </PopoverTrigger>
        <PopoverContent className="w-[300px] p-0">
          <Command>
            <CommandInput placeholder="Search Collections..." />
            <CommandEmpty>No Collection found.</CommandEmpty>
            <CommandGroup>
            {collections.map((collection) => (
                <CommandItem
                  key={collection.id}
                  value={collection.name}
                  onSelect={(currentValue) => {
                    setValue(currentValue === value ? "" : currentValue)
                    setOpen(false)
                  }}
                >
                  <Check
                    className={cn(
                      "mr-2 h-4 w-4",
                      value === collection.name.toLowerCase() ? "opacity-100" : "opacity-0"
                    )}
                  />
                  <Avatar src={collection.profile_pic} className="h-5 w-5 mr-2"/>
                  {collection.name}
                </CommandItem>
              ))}
            </CommandGroup>
          </Command>
        </PopoverContent>
      </Popover>
    )
  }


export const UsersFilter = (props: Props) => {
    const [open, setOpen] = useState(false)
    const [value, setValue] = useState("")
    return (
        <Popover open={open} onOpenChange={setOpen}>
        <PopoverTrigger >
        <Badge className="flex items-center gap-2 py-1 bg-secondary hover:bg-secondary/80 text-secondary-foreground">
        <LuUser2 size={25}/>
        {value
              ? users.find((user) => user.name.toLowerCase() === value)?.name
              :<TypographySmall className='text-foreground' text='User'/>
            }
            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
        </Badge>
        </PopoverTrigger>
        <PopoverContent className="w-[300px] p-0">
          <Command>
            <CommandInput placeholder="Search Users..." />
            <CommandEmpty>No User found.</CommandEmpty>
            <CommandGroup>
              {users.map((user) => (
                <CommandItem
                  key={user.id}
                  value={user.name}
                  onSelect={(currentValue) => {
                    setValue(currentValue === value ? "" : currentValue)
                    setOpen(false)
                  }}
                >
                  <Check
                    className={cn(
                      "mr-2 h-4 w-4",
                      value === user.name.toLowerCase() ? "opacity-100" : "opacity-0"
                    )}
                  />
                  <Avatar className="h-5 w-5 mr-2"/>
                  {user.name}
                </CommandItem>
              ))}
            </CommandGroup>
          </Command>
        </PopoverContent>
      </Popover>
    )
  }
