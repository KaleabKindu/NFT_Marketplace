"use client";
import { Badge } from "../ui/badge";
import { IoWalletOutline, IoChevronDown } from "react-icons/io5";
import { TypographyP, TypographySmall } from "../common/Typography";
import { Button } from "../ui/button";
import { IoMdCloseCircle } from "react-icons/io";
import { Slider } from "@/components/ui/slider";
import { BiSortAlt2, BiCategory } from "react-icons/bi";
import { FILTER, categories, sale_types, sort_types } from "@/data";
import { Checkbox } from "../ui/checkbox";
import { Label } from "@/components/ui/label";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { ReactNode, useCallback, useEffect, useState } from "react";
import { MdOutlineSell } from "react-icons/md";
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
import { useGetCollectionsQuery, useGetUsersQuery } from "@/store/api";
import { ICollection, User } from "@/types";

type SearchProps = {
  className?: string;
  postIcon?: ReactNode;
};

export const SearchInput = ({ className, postIcon }: SearchProps) => {
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
    updateQueryParameter(value, FILTER.SEARCH);
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
      {postIcon && (
        <Button
          className="absolute top-0 bottom-0 my-auto right-0 rounded-full"
          size={"icon"}
        >
          {postIcon}
        </Button>
      )}
    </div>
  );
};

export const SaleFilter = () => {
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
    updateQueryParameter(selectedSaleType, FILTER.SALE);
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

export const PriceFilter = () => {
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
      ? newParams.set(FILTER.MIN_PRICE, debminPrice.toString())
      : newParams.delete(FILTER.MIN_PRICE);
    debmaxPrice
      ? newParams.set(FILTER.MAX_PRICE, debmaxPrice.toString())
      : newParams.delete(FILTER.MAX_PRICE);
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

export const CategoryFilter = () => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState<string | undefined>(
    undefined,
  );
  const handleChange = (status: boolean, value: string) => {
    if (status) {
      setSelectedCategory(value);
      updateQueryParameter(FILTER.CATEGORY, value);
    } else {
      setSelectedCategory(undefined);
      updateQueryParameter(FILTER.CATEGORY);
    }
  };
  const updateQueryParameter = useCallback(
    (key: string, value?: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );
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
              setSelectedCategory(undefined);
            }}
          >
            {selectedCategory ? (
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
              checked={selectedCategory === "all"}
              onCheckedChange={(val: boolean) => handleChange(val, "all")}
            />
            <TypographyP text="All" />
          </div>
          {categories.map((category, index) => (
            <div key={index} className="flex gap-3 items-center">
              <Checkbox
                checked={selectedCategory === category.value}
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
export const CategoryFilter2 = () => {
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [active, setActive] = useState<string | undefined>(undefined);
  const handleChange = (value?: string) => {
    setActive(value);
    updateQueryParameter(FILTER.CATEGORY, value);
  };
  const updateQueryParameter = useCallback(
    (key: string, value?: string) => {
      const newParams = new URLSearchParams(params.toString());
      value ? newParams.set(key, value) : newParams.delete(key);
      router.push(`${pathname}?${newParams.toString()}`);
    },
    [params],
  );
  return (
    <div className="flex flex-wrap gap-2">
      <Button
        type="button"
        onClick={() => handleChange(undefined)}
        size={"sm"}
        className="rounded-full px-4 py-0.5 text-sm"
        variant={!active ? "default" : "outline"}
      >
        All
      </Button>
      {categories.map((category, index) => (
        <Button
          type="button"
          key={index}
          onClick={() => handleChange(category.value)}
          size={"sm"}
          className="rounded-full px-4 py-0.5 text-sm"
          variant={active === category.value ? "default" : "outline"}
        >
          {category.name}
        </Button>
      ))}
    </div>
  );
};

export const SortFilter = () => {
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
    updateQueryParameter(sort_types[index]?.value, FILTER.SORT_BY);
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
            text={index > -1 ? sort_types[index].name : "Sort By"}
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

export const CollectionsFilter = () => {
  const [collections, setCollections] = useState<ICollection[]>([]);
  const [query, setQuery] = useState("");
  const [value, setValue] = useState("");
  const { data } = useGetCollectionsQuery({
    search: value,
    pageNumber: 1,
    pageSize: 5,
  });
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
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
    updateQueryParameter(value, FILTER.COLLECTION);
  }, [value]);
  useEffect(() => {
    if (data) {
      setCollections([...data.value]);
    }
  }, [data]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button className="flex items-center gap-2 py-1 bg-secondary rounded-full hover:bg-secondary/80 text-secondary-foreground">
          <MdOutlineCollectionsBookmark size={25} />
          <TypographySmall
            className="text-foreground"
            text={
              value
                ? collections.find((collection) => collection.id === value)
                    ?.name
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
          <CommandInput
            placeholder="Search Collections..."
            onInput={(e) => setQuery((e.target as HTMLInputElement).value)}
          />
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
                    value === collection.id ? "opacity-100" : "opacity-0",
                  )}
                />
                <Avatar className="h-5 w-5 mr-2" />
                {collection.name}
              </CommandItem>
            ))}
          </CommandGroup>
        </Command>
      </PopoverContent>
    </Popover>
  );
};

export const UsersFilter = () => {
  const [query, setQuery] = useState("");
  const [users, setUsers] = useState<User[]>([]);
  const { data } = useGetUsersQuery({
    search: query,
    pageNumber: 1,
    pageSize: 5,
  });
  const [value, setValue] = useState("");
  const router = useRouter();
  const pathname = usePathname();
  const params = useSearchParams();
  const [open, setOpen] = useState(false);
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
  const currentUser = users?.find((user) => user.address === value);

  useEffect(() => {
    updateQueryParameter(value, FILTER.CREATOR);
  }, [value]);
  useEffect(() => {
    if (data) {
      setUsers([...data.value]);
    }
  }, [data]);
  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger>
        <Button className="flex items-center gap-2 h-auto bg-secondary rounded-full hover:bg-secondary/80 text-secondary-foreground">
          <LuUser2 size={25} />
          <TypographySmall
            className="text-foreground"
            text={
              value
                ? currentUser?.username
                  ? currentUser?.username.slice(0, 10)
                  : currentUser?.address.slice(2, 7)
                : "Creators"
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
          <CommandInput
            placeholder="Search Users..."
            onInput={(e) => setQuery((e.target as HTMLInputElement).value)}
          />
          <CommandEmpty>No User found.</CommandEmpty>
          <CommandGroup>
            {users.map((user) => (
              <CommandItem
                key={user.address}
                value={user.address}
                onSelect={(currentValue) => {
                  setValue(currentValue === value ? "" : currentValue);
                  setOpen(false);
                }}
              >
                <Check
                  className={cn(
                    "mr-2 h-4 w-4",
                    value === user.address ? "opacity-100" : "opacity-0",
                  )}
                />
                <Avatar className="h-5 w-5 mr-2" />
                {user.username
                  ? user.username.slice(0, 10)
                  : user.address.slice(2, 7)}
              </CommandItem>
            ))}
          </CommandGroup>
        </Command>
      </PopoverContent>
    </Popover>
  );
};
