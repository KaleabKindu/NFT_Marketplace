import {
  PriceFilter,
  CreatorsFilter,
  SearchInput,
  SortFilter,
} from "@/components/common/SearchFilters";

type Props = {};

const Filters = (props: Props) => {
  return (
    <div>
      <div className="flex flex-col lg:flex-row gap-8 items-center mt-16">
        <SearchInput className="flex-1" />
        <div className="flex-1 flex flex-wrap items-center gap-5">
          <PriceFilter />
          <CreatorsFilter />
          {/* <SortFilter /> */}
        </div>
      </div>
    </div>
  );
};

export default Filters;
