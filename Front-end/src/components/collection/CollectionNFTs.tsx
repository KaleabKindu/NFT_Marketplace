"use client";
import NFTCard from "../explore/assets/NFTCard";
import { useInView } from "react-intersection-observer";
import { useState, useEffect } from "react";
import { NFT } from "@/types";
import { useGetAssetsQuery } from "@/store/api";
import AssetsShimmers from "../common/shimmers/AssetsShimmers";
import NoData from "../common/NoData";
import Error from "../common/Error";
import { useParams, useSearchParams } from "next/navigation";
import { FILTER } from "@/data";
import Pagination from "@/components/common/Pagination";

type Props = {};

const CollectionNFTs = (props: Props) => {
  const { id } = useParams();
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isLoading, isFetching, isError, refetch } = useGetAssetsQuery({
    search: params.get(FILTER.SEARCH) as string,
    category: params.get(FILTER.CATEGORY) as string,
    min_price: params.get(FILTER.MIN_PRICE) as string,
    max_price: params.get(FILTER.MAX_PRICE) as string,
    sale_type: params.get(FILTER.SALE) as string,
    sort_by: params.get(FILTER.SORT_BY) as string,
    collection: id as string,
    pageNumber: page,
    pageSize: size,
  });
  const [assets, setAssets] = useState<NFT[]>([]);
  const { ref, inView } = useInView({ threshold: 0.3 });
  useEffect(() => {
    if (data && page * size > assets.length) {
      setAssets([...assets, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && !(page * size >= total)) {
      setPage(page + 1);
    }
  }, [inView]);
  return (
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
          <Pagination
            total={100}
            currentPage={page}
            setPage={(a: number) => {
              setPage(a);
              window.scrollTo({ top: 500, behavior: "smooth" });
            }}
          />
        </>
      ) : (
        <NoData message="No assets found" />
      )}
    </div>
  );
};

export default CollectionNFTs;
