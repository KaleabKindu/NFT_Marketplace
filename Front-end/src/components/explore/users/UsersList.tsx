"use client";
import { Creator } from "@/components/landing-page/TopCreators";
import { users as usersData } from "@/utils";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import { useGetUsersQuery } from "@/store/api";
import UsersShimmers from "@/components/common/shimmers/UsersShimmers";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { User } from "@/types";
import Pagination from "@/components/common/Pagination";
import { FILTER } from "@/data";
type Props = {};

const UsersList = (props: Props) => {
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isFetching, isError, refetch } = useGetUsersQuery({
    search: params.get(FILTER.SEARCH) as string,
    pageNumber: page,
    pageSize: size,
  });
  const [users, setUsers] = useState<User[]>([]);
  useEffect(() => {
    if (data) {
      setUsers([...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  return (
    <>
      <div className="grid grid-cols-12 items-center justify-center gap-5">
        {isFetching ? (
          <UsersShimmers elements={size} />
        ) : isError ? (
          <Error retry={refetch} />
        ) : users && users.length > 0 ? (
          <>
            {users.map((user, index) => (
              <Creator key={index} user={user} index={index} showRank={false} />
            ))}
            {total > size && (
              <Pagination
                total={total}
                offset={size}
                currentPage={page}
                setPage={(a: number) => {
                  setPage(a);
                  window.scrollTo({ top: 0, behavior: "smooth" });
                }}
              />
            )}
          </>
        ) : (
          <NoData message="No Users found" />
        )}
      </div>
    </>
  );
};

export default UsersList;
