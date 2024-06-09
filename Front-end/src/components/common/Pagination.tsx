import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";

type Props = {
  setPage: (a: number) => void;
  total: number;
  currentPage: number;
  offset?: number;
};
export default function CPagination({
  setPage,
  total,
  currentPage,
  offset = 10,
}: Props) {
  const pages = Math.ceil(total / offset);
  const forward = () => {
    setPage(currentPage + 1);
  };
  const backward = () => {
    currentPage > 1 && setPage(currentPage - 1);
  };

  return (
    <Pagination className="col-span-12 flex justify-end mt-10">
      <PaginationContent>
        <PaginationPrevious className="rounded-full p-2" onClick={backward} />
        {pages <= 5 ? (
          <>
            {Array.from({ length: pages }).map((_, index) => (
              <PageNumber
                key={index}
                setPage={setPage}
                active={currentPage === index + 1}
              >{`${index + 1}`}</PageNumber>
            ))}
          </>
        ) : currentPage <= 4 ? (
          <>
            <PageNumber setPage={setPage} active={currentPage === 1}>
              1
            </PageNumber>
            <PageNumber setPage={setPage} active={currentPage === 2}>
              2
            </PageNumber>
            <PageNumber setPage={setPage} active={currentPage === 3}>
              3
            </PageNumber>
            <PageNumber setPage={setPage} active={currentPage === 4}>
              4
            </PageNumber>
            <PaginationItem>
              <PaginationEllipsis />
            </PaginationItem>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages}
            >{`${pages}`}</PageNumber>
          </>
        ) : currentPage >= pages - 3 ? (
          <>
            <PageNumber setPage={setPage} active={currentPage === 1}>
              1
            </PageNumber>
            <PaginationItem>
              <PaginationEllipsis />
            </PaginationItem>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages - 3}
            >{`${pages - 3}`}</PageNumber>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages - 2}
            >{`${pages - 2}`}</PageNumber>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages - 1}
            >{`${pages - 1}`}</PageNumber>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages}
            >{`${pages}`}</PageNumber>
          </>
        ) : (
          <>
            <PageNumber setPage={setPage} active={currentPage === 1}>
              1
            </PageNumber>
            <PaginationItem>
              <PaginationEllipsis />
            </PaginationItem>
            <PageNumber
              setPage={setPage}
              active={currentPage === currentPage - 1}
            >{`${currentPage - 1}`}</PageNumber>
            <PageNumber
              setPage={setPage}
              active={currentPage === currentPage}
            >{`${currentPage}`}</PageNumber>
            <PageNumber
              setPage={setPage}
              active={currentPage === currentPage + 1}
            >{`${currentPage + 1}`}</PageNumber>
            <PaginationItem>
              <PaginationEllipsis />
            </PaginationItem>
            <PageNumber
              setPage={setPage}
              active={currentPage === pages}
            >{`${pages}`}</PageNumber>
          </>
        )}
        <PaginationNext className="rounded-full p-2" onClick={forward} />
      </PaginationContent>
    </Pagination>
  );
}

type PageType = {
  children: string;
  active: boolean;
  setPage: (a: number) => void;
};
const PageNumber = ({ children, active, setPage }: PageType) => {
  return (
    <PaginationItem onClick={() => setPage(parseInt(children))}>
      <PaginationLink isActive={active}>{children}</PaginationLink>
    </PaginationItem>
  );
};
