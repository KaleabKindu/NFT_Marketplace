"use client";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Badge } from "../ui/badge";
import {
  IoWalletOutline,
  IoChevronDown,
  IoRadioButtonOff,
} from "react-icons/io5";
import { TypographyP, TypographySmall } from "../common/Typography";
import { Button } from "../ui/button";
import { IoMdCloseCircle } from "react-icons/io";
import { Slider } from "@/components/ui/slider";
import { BiSortAlt2, BiCategory } from "react-icons/bi";
import {
  categories,
  category_type,
  collections,
  sale_types,
  sort_types,
  users,
} from "@/data";
import { Checkbox } from "../ui/checkbox";
import { Label } from "@/components/ui/label";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { useCallback, useEffect, useState } from "react";
import { MdOutlineSell, MdOutlineSportsCricket } from "react-icons/md";
import { cn } from "@/lib/utils";
import { CiSearch } from "react-icons/ci";
import { Input } from "../ui/input";
import { Check, ChevronsUpDown } from "lucide-react";
import { MdOutlineCollectionsBookmark } from "react-icons/md";
import { LuUser2 } from "react-icons/lu";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Avatar } from "../common/Avatar";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { useDebounce } from "use-debounce";

type Props = {};

const SearchFilter = (props: Props) => {
  return (
    <div className="flex flex-col lg:flex-row gap-3 items-center mt-16">
      <SearchInput className="flex-1" />
      <div className="flex-1 flex flex-wrap lg:flex-nowrap justify-center items-center gap-2">
        <PriceFilter />
        <SaleFilter />
        <CategoryFilter />
        <SortFilter />
      </div>
    </div>
  );
};

export default SearchFilter;

type SearchProps = {
  className?: string;
};

export const SearchInput = ({ className }: SearchProps) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [query, setQuery] = useState("");
  const [value] = useDebounce(query, 1000);
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );

  useEffect(() => {
    updateQueryParameter(value, "search");
  }, [value]);
  return (
    <div className={cn("relative", className)}>
      <CiSearch className="absolute top-0 bottom-0 my-auto left-3" size={25} />
      <Input
        type="text"
        placeholder="Search"
        onChange={(e) => setQuery(e.target.value)}
        className="rounded-full pl-12 pr-4 bg-accent text-accent-foreground focus:border-background/80"
      />
    </div>
  );
};

export const SaleFilter = (props: Props) => {
  const params = useSearchParams();
  const router = useRouter();
  const pathname = usePathname();
  const [open, setOpen] = useState(false);
  const [selectedSaleType, setSelectedSaleType] = useState<string>("");
  const handleChange = (status: boolean, value: string) => {
    if (status) {
      setSelectedSaleType(value);
    } else {
      setSelectedSaleType("");
    }
  };
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );
  useEffect(() => {
    updateQueryParameter(selectedSaleType, "sale_type");
  }, [selectedSaleType]);

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button
          variant={"secondary"}
          className="flex items-center gap-2 py-1 rounded-full"
        >
          <MdOutlineSell size={25} />
          <TypographySmall
            className="text-foreground capitalize"
            text={selectedSaleType ? selectedSaleType : "Sale Type"}
          />
          <Badge
            variant={"secondary"}
            className="p-0 h-auto rounded-full"
            onClick={() => setSelectedSaleType("")}
          >
            {selectedSaleType ? (
              <IoMdCloseCircle size={25} />
            ) : (
              <IoChevronDown size={20} />
            )}
          </Badge>
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[300px] p-0 rounded-xl">
        <div className="flex flex-col gap-3 w-[20rem] p-5">
          <div className="flex gap-3 items-center">
            <Checkbox
              checked={selectedSaleType === "all"}
              onCheckedChange={(val: boolean) => handleChange(val, "all")}
            />
            <TypographyP text="All" />
          </div>
          {sale_types.map((sale, index) => (
            <div key={index} className="flex gap-3 items-center">
              <Checkbox
                checked={selectedSaleType === sale.value}
                onCheckedChange={(val: boolean) =>
                  handleChange(val, sale.value)
                }
              />
              <TypographyP text={sale.name} />
            </div>
          ))}
        </div>
      </PopoverContent>
    </Popover>
  );
};

export const PriceFilter = (props: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(0);
  const [debminPrice] = useDebounce(minPrice, 1000);
  const [debmaxPrice] = useDebounce(maxPrice, 1000);
  const handlePriceChange = (range: number[]) => {
    setMinPrice(range[0]);
    setMaxPrice(range[1]);
  };

  const handleClose = (e: any) => {
    e.preventDefault();
    setMinPrice(0);
    setMaxPrice(0);
  };

  useEffect(() => {
    const newParams = new URLSearchParams(params.toString());
    debminPrice
      ? newParams.set("min_price", debminPrice.toString())
      : newParams.delete("min_price");
    debmaxPrice
      ? newParams.set("max_price", debmaxPrice.toString())
      : newParams.delete("max_price");
    router.push(`${pathname}?${newParams.toString()}`);
  }, [debminPrice, debmaxPrice]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button
          variant="secondary"
          className="flex items-center gap-2 py-1 rounded-full"
        >
          <IoWalletOutline size={25} />
          <TypographySmall
            className="text-foreground"
            text={`${minPrice}ETH - ${maxPrice}ETH`}
          />
          <Badge variant="secondary" className="p-0 h-auto rounded-full">
            {minPrice + maxPrice != 0 ? (
              <IoMdCloseCircle
                onClick={handleClose}
                className="cursor-pointer"
                size={25}
              />
            ) : (
              <IoChevronDown size={20} />
            )}
          </Badge>
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-fit p-0 rounded-xl">
        <div className="rounded-xl bg-background z-20 mt-3">
          <div className="flex flex-col gap-5 w-[20rem] p-5">
            <TypographyP text="Price Range" />
            <Slider
              onValueChange={handlePriceChange}
              minStepsBetweenThumbs={1}
              value={[minPrice, maxPrice]}
              step={0.1}
            />
            <div className="flex justify-between">
              <div className="flex flex-col gap-1.5 w-[40%]">
                <TypographySmall className="ml-2" text="Min Price" />
                <div className="flex justify-between gap-2 items-center ">
                  <Input
                    type="number"
                    onChange={(e) => setMinPrice(parseFloat(e.target.value))}
                    className="rounded-xl bg-transparent"
                    value={minPrice}
                  />
                  <TypographySmall text="ETH" />
                </div>
              </div>
              <div className="flex flex-col gap-1.5 w-[40%]">
                <TypographySmall className="mr-2 self-end" text="Max Price" />
                <div className="flex justify-between items-center gap-2">
                  <Input
                    type="number"
                    onChange={(e) => setMaxPrice(parseFloat(e.target.value))}
                    className="rounded-xl bg-transparent"
                    value={maxPrice}
                  />
                  <TypographySmall text="ETH" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </PopoverContent>
    </Popover>
  );
};

export const CategoryFilter = (props: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
  const handleChange = (status: boolean, value: string) => {
    if (status) {
      setSelectedCategories([...selectedCategories, value]);
    } else {
      setSelectedCategories(selectedCategories.filter((val) => val !== value));
    }
  };
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );

  useEffect(() => {
    updateQueryParameter(selectedCategories.join(","), "categories");
  }, [selectedCategories]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button
          variant="secondary"
          className="flex items-center gap-2 py-1 rounded-full"
        >
          <BiCategory size={25} />
          <TypographySmall className="text-foreground" text="Category" />
          <Badge
            variant={"secondary"}
            className="p-0 h-auto rounded-full"
            onClick={(e) => {
              e.preventDefault();
              setSelectedCategories([]);
            }}
          >
            {selectedCategories.length > 0 ? (
              <IoMdCloseCircle size={25} />
            ) : (
              <IoChevronDown size={20} />
            )}
          </Badge>
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[300px] p-0 rounded-xl">
        <div className="flex flex-col gap-3 w-[20rem] p-5">
          <div className="flex gap-3 items-center">
            <Checkbox
              checked={selectedCategories.includes("all")}
              onCheckedChange={(val: boolean) => handleChange(val, "all")}
            />
            <TypographyP text="All" />
          </div>
          {category_type.map((category, index) => (
            <div key={index} className="flex gap-3 items-center">
              <Checkbox
                checked={selectedCategories.includes(category.value)}
                onCheckedChange={(val: boolean) =>
                  handleChange(val, category.value)
                }
              />
              <TypographyP text={category.name} />
            </div>
          ))}
        </div>
      </PopoverContent>
    </Popover>
  );
};

export const SortFilter = (props: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [index, setIndex] = useState(-1);
  const handleChange = (value: string) => {
    setIndex(parseInt(value));
  };
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );

  useEffect(() => {
    updateQueryParameter(sort_types[index]?.value, "sort_by");
  }, [index]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button
          variant={"secondary"}
          className="flex items-center gap-2 py-1 rounded-full"
        >
          <BiSortAlt2 size={25} />
          <TypographySmall
            className="text-foreground capitalize"
            text={index > -1 ? sort_types[index].name : "Sale Type"}
          />
          <Badge
            variant={"secondary"}
            className="p-0 h-auto rounded-full"
            onClick={() => setIndex(-1)}
          >
            {index > -1 ? (
              <IoMdCloseCircle size={25} />
            ) : (
              <IoChevronDown size={20} />
            )}
          </Badge>
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[300px] p-0 rounded-xl">
        <RadioGroup
          className="p-5 w-[20rem]"
          value={index.toString()}
          onValueChange={(e) => handleChange(e)}
          defaultValue="option-one"
        >
          {sort_types.map((type, index) => (
            <div key={index} className="flex items-center space-x-2">
              <RadioGroupItem
                className="w-5 h-5"
                value={index.toString()}
                id={type.value}
              />
              <Label className="text-md" htmlFor={type.value}>
                {type.name}
              </Label>
            </div>
          ))}
        </RadioGroup>
      </PopoverContent>
    </Popover>
  );
};

export const CollectionsFilter = (props: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [value, setValue] = useState("");
  const handleClear = (e: any) => {
    e.preventDefault();
    setValue("");
  };
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );

  useEffect(() => {
    updateQueryParameter(value, "collection");
  }, [value]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button className="flex items-center gap-2 py-1 bg-secondary rounded-full hover:bg-secondary/80 text-secondary-foreground">
          <MdOutlineCollectionsBookmark size={25} />
          <TypographySmall
            className="text-foreground"
            text={
              value
                ? collections.find(
                    (collection) => collection.name.toLowerCase() === value,
                  )?.name
                : "Collections"
            }
          />
          {value ? (
            <IoMdCloseCircle
              className="cursor-pointer"
              onClick={handleClear}
              size={25}
            />
          ) : (
            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
          )}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[300px] p-1 rounded-xl">
        <Command>
          <CommandInput placeholder="Search Collections..." />
          <CommandEmpty>No Collection found.</CommandEmpty>
          <CommandGroup>
            {collections.map((collection) => (
              <CommandItem
                key={collection.id}
                value={collection.name}
                onSelect={(currentValue) => {
                  setValue(currentValue === value ? "" : currentValue);
                  setOpen(false);
                }}
              >
                <Check
                  className={cn(
                    "mr-2 h-4 w-4",
                    value === collection.name.toLowerCase()
                      ? "opacity-100"
                      : "opacity-0",
                  )}
                />
                <Avatar src={collection.profile_pic} className="h-5 w-5 mr-2" />
                {collection.name}
              </CommandItem>
            ))}
          </CommandGroup>
        </Command>
      </PopoverContent>
    </Popover>
  );
};

export const UsersFilter = (props: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [value, setValue] = useState("");
  const handleClear = (e: any) => {
    e.preventDefault();
    setValue("");
  };
  const updateQueryParameter = useCallback(
    (value: string, key: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );

  useEffect(() => {
    updateQueryParameter(value, "creator");
  }, [value]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button className="flex items-center gap-2 h-auto bg-secondary rounded-full hover:bg-secondary/80 text-secondary-foreground">
          <LuUser2 size={25} />
          <TypographySmall
            className="text-foreground"
            text={
              value
                ? users.find((user) => user.name.toLowerCase() === value)?.name
                : "Users"
            }
          />
          {value ? (
            <IoMdCloseCircle
              className="cursor-pointer"
              onClick={handleClear}
              size={25}
            />
          ) : (
            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
          )}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-fit p-1 rounded-xl">
        <Command>
          <CommandInput placeholder="Search Users..." />
          <CommandEmpty>No User found.</CommandEmpty>
          <CommandGroup>
            {users.map((user) => (
              <CommandItem
                key={user.id}
                value={user.name}
                onSelect={(currentValue) => {
                  setValue(currentValue === value ? "" : currentValue);
                  setOpen(false);
                }}
              >
                <Check
                  className={cn(
                    "mr-2 h-4 w-4",
                    value === user.name.toLowerCase()
                      ? "opacity-100"
                      : "opacity-0",
                  )}
                />
                <Avatar className="h-5 w-5 mr-2" />
                {user.name}
              </CommandItem>
            ))}
          </CommandGroup>
        </Command>
      </PopoverContent>
    </Popover>
  );
};
