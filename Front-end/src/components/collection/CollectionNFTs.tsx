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

type Props = {};

const CollectionNFTs = (props: Props) => {
  const { id } = useParams();
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const [fetchingNextPage, setFetchingNextPage] = useState(false);
  const { data, isLoading, isFetching, isError, refetch } = useGetAssetsQuery({
    search: params.get(FILTER.SEARCH) as string,
    category: params.get(FILTER.CATEGORY) as string,
    min_price: params.get(FILTER.MIN_PRICE) as string,
    max_price: params.get(FILTER.MAX_PRICE) as string,
    sale_type: params.get(FILTER.SALE) as string,
    sort_by: params.get(FILTER.SORT_BY) as string,
    collectionId: id as string,
    pageNumber: page,
    pageSize: size,
  });
  const [assets, setAssets] = useState<NFT[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });
  useEffect(() => {
    if (data) {
      setAssets([...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && size < total) {
      setSize(size * 2);
      setFetchingNextPage(true);
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
          {assets.map((asset, index) => (
            <NFTCard key={index} asset={asset} />
          ))}
          {isFetching && fetchingNextPage && <AssetsShimmers elements={size} />}
          <div ref={ref} />
        </>
      ) : (
        <NoData message="No assets found" />
      )}
    </div>
  );
};

export default CollectionNFTs;
