"use client";
import { TbFilterSearch } from "react-icons/tb";
import { IoChevronDown } from "react-icons/io5";
import { Button } from "../../ui/button";
import {
  PriceFilter,
  SaleFilter,
  SortFilter,
  CollectionsFilter,
  UsersFilter,
  CategoryFilter2,
} from "@/components/common/SearchFilters";
import { useState } from "react";
import { cn } from "@/lib/utils";

type Props = {};

const Filters = (props: Props) => {
  const [showFilter, setShowFilter] = useState(false);
  return (
    <div className="flex flex-col gap-5 w-full mt-8 lg:mt-16">
      <div className="flex items-center justify-between border-b py-6">
        <CategoryFilter2 />
        <Button
          className="rounded-full"
          onClick={() => setShowFilter(!showFilter)}
        >
          <TbFilterSearch className="mr-2" size={25} />
          Filter
          <IoChevronDown
            className={cn("ml-2", { "transform rotate-180": showFilter })}
            size={25}
          />
        </Button>
      </div>
      {showFilter && (
        <div className="flex flex-wrap items-center gap-5">
          <PriceFilter />
          <SaleFilter />
          <CollectionsFilter />
          <UsersFilter />
          <SortFilter />
        </div>
      )}
    </div>
  );
};

export default Filters;
