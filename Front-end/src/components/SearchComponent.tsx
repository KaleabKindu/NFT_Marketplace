import { cn } from '@/lib/utils'
import { Input } from './ui/input'
import { CiSearch } from 'react-icons/ci'
type Props = {
    className?:string
}

const SearchComponent = ({className}: Props) => {
  return (
    <div className={cn("relative", className)}>
        <CiSearch className="absolute top-0 bottom-0 my-auto left-3" size={25}/>
        <Input type="text" placeholder="Search" className="rounded-full pl-12 pr-4 bg-accent text-accent-foreground focus:border-background/80" />
    </div>

  )
}

export default SearchComponent