'use client'
import { CiSearch } from "react-icons/ci";
import { Input } from "../../ui/input";
import { cn } from "@/lib/utils";
import { Button } from "../../ui/button";
import { MdArrowForward } from "react-icons/md";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { useDebounce } from "use-debounce"
import { useEffect, useState, useCallback } from "react";
type Props = {
  className?: string;
};

const SearchBar = ({ className }: Props) => {
  const router = useRouter()
  const pathname = usePathname()
  const params = useSearchParams() 
  const [ query, setQuery ] = useState('')
  const [ value ] = useDebounce(query, 1000)
  const updateQueryParameter = useCallback((value:string, key:string) => {
    const newParams = new URLSearchParams(params.toString())
    value ? newParams.set(key, value):newParams.delete(key)
    router.push(`${pathname}?${newParams.toString()}`)
  },[params])

  useEffect(() => {
    updateQueryParameter(value, 'search')
  },[value])
  return (
    <div className={cn("relative", className)}>
      <CiSearch className="absolute top-0 bottom-0 my-auto left-3" size={25} />
      <Input
        type="text"
        placeholder="Search"
        onChange={(e) => setQuery(e.target.value)}
        className="rounded-full h-12 pl-12 pr-12 bg-accent text-accent-foreground focus:border-background/80"
      />
      <Button
        className="absolute top-0 bottom-0 my-auto right-2 rounded-full"
        size={"icon"}
      >
        <MdArrowForward size={30} />
      </Button>
    </div>
  );
};

export default SearchBar;
