import {
  CategoryFilter,
  PriceFilter,
  SaleFilter,
  SearchInput,
  SortFilter,
} from "../common/SearchFilters";

type Props = {};

const Filters = (props: Props) => {
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

export default Filters;
