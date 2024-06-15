"use client";
import { assets as assetsData } from "@/utils";
import NFTCard from "./NFTCard";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "@/components/common/shimmers/AssetsShimmers";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { NFT } from "@/types";
import Pagination from "@/components/common/Pagination";
import { FILTER } from "@/data";
type Props = {};

const NFTList = (props: Props) => {
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isFetching, isError, refetch } = useGetAssetsQuery({
    search: params.get(FILTER.SEARCH) as string,
    category: params.get(FILTER.CATEGORY) as string,
    min_price: params.get(FILTER.MIN_PRICE) as string,
    max_price: params.get(FILTER.MAX_PRICE) as string,
    sale_type: params.get(FILTER.SALE) as string,
    sort_by: params.get(FILTER.SORT_BY) as string,
    collectionId: params.get(FILTER.COLLECTION) as string,
    creator: params.get(FILTER.CREATOR) as string,
    searchQuery: params.get(FILTER.SEMANTIC_SEARCH) as string,
    pageNumber: page,
    pageSize: size,
  });
  const [assets, setAssets] = useState<NFT[]>([]);
  useEffect(() => {
    if (data) {
      setAssets([...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  return (
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isFetching ? (
          <AssetsShimmers elements={size} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : assets && assets.length > 0 ? (
          <>
            {assets.slice(0, size).map((asset, index) => (
              <NFTCard key={index} asset={asset} />
            ))}
            {total > size && (
              <Pagination
                total={total}
                currentPage={page}
                offset={size}
                setPage={(a: number) => {
                  setPage(a);
                  window.scrollTo({ top: 0, behavior: "smooth" });
                }}
              />
            )}
          </>
        ) : (
          <NoData message="No assets found" />
        )}
      </div>
    </>
  );
};

export default NFTList;
