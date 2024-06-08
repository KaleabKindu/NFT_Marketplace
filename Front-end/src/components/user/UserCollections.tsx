import CollectionCard from "../CollectionCard";
import { useParams } from "next/navigation";
import { useInView } from "react-intersection-observer";
import { useEffect, useState } from "react";
import { useGetCollectionsQuery } from "@/store/api";
import { ICollection } from "@/types";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import CollectionsShimmers from "../common/shimmers/CollectionShimmers";

type Props = {};

const UserCollections = (props: Props) => {
  const { address } = useParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isLoading, isFetching, isError, refetch } =
    useGetCollectionsQuery({
      creator: address as string,
      pageNumber: page,
      pageSize: size,
    });
  const [collections, setCollections] = useState<ICollection[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });

  useEffect(() => {
    if (data) {
      setCollections([...collections, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && !(page * size >= total)) {
      setPage(page + 1);
    }
  }, [inView]);
  return (
    <>
      <div className="grid grid-cols-12 justify-center items-center gap-5">
        {isLoading ? (
          <CollectionsShimmers elements={size} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : collections && collections.length > 0 ? (
          <>
            {collections.map((collection, index) => (
              <CollectionCard key={index} collection={collection} />
            ))}
            {isFetching && <CollectionsShimmers elements={size} />}
            <div ref={ref} />
          </>
        ) : (
          <NoData message="No Collections found" />
        )}
      </div>
    </>
  );
};

export default UserCollections;
