"use client";
import { TbFilterSearch } from "react-icons/tb";
import { IoChevronDown } from "react-icons/io5";
import { Button } from "../../ui/button";
import {
  PriceFilter,
  SaleFilter,
  SortFilter,
  CollectionsFilter,
  CategoryFilter2,
  CreatorsFilter,
  SemanticSearch,
} from "@/components/common/SearchFilters";
import { useState } from "react";
import { cn } from "@/lib/utils";

type Props = {};

const Filters = (props: Props) => {
  const [showFilter, setShowFilter] = useState(false);
  return (
    <div className="flex flex-col gap-5 w-full mt-8 lg:mt-16">
      <div className="flex flex-wrap gap-3 items-center justify-between border-b py-6">
        <CategoryFilter2 />
        <Button
          className="rounded-full ml-auto"
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
        <div className="flex flex-wrap justify-center gap-5">
          <SemanticSearch />
          <div className="flex flex-wrap items-center gap-5">
            <PriceFilter />
            <SaleFilter />
            <CollectionsFilter />
            <CreatorsFilter />
            <SortFilter />
          </div>
        </div>
      )}
    </div>
  );
};

export default Filters;
