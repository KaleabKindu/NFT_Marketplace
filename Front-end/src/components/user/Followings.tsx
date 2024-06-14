"use client";
import { Creator } from "@/components/landing-page/TopCreators";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import { useGetUserNetworksQuery } from "@/store/api";
import UsersShimmers from "@/components/common/shimmers/UsersShimmers";
import { useParams } from "next/navigation";
import { useInView } from "react-intersection-observer";
import { useEffect, useState } from "react";
import { User } from "@/types";
type Props = {};

const Followings = (props: Props) => {
  const params = useParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(8);
  const [fetchingNextPage, setFetchingNextPage] = useState(false);
  const { data, isFetching, isLoading, isError, refetch } =
    useGetUserNetworksQuery({
      type: "followings",
      address: params.address as string,
      pageNumber: page,
      pageSize: size,
    });
  const [users, setUsers] = useState<User[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });
  useEffect(() => {
    if (data) {
      setUsers([...data.value]);
      setFetchingNextPage(false);
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
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isLoading ? (
          <UsersShimmers elements={size} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : users && users.length > 0 ? (
          <>
            {users.map((user, index) => (
              <Creator key={index} user={user} index={index} showRank={false} />
            ))}
            {isFetching && fetchingNextPage && (
              <UsersShimmers elements={size} />
            )}
            <div ref={ref} />
          </>
        ) : (
          <NoData message="No Users found" />
        )}
      </div>
    </>
  );
};

export default Followings;
