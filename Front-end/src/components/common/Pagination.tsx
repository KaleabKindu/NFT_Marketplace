import clsx from "clsx";
import {
    Pagination,
    PaginationContent,
    PaginationEllipsis,
    PaginationItem,
    PaginationLink,
    PaginationNext,
    PaginationPrevious,
  } from "@/components/ui/pagination"
import { IoIosArrowBack, IoIosArrowForward } from "react-icons/io";

type Props = {
    column?:boolean,
    setPage: (a:number) => void,
    total:number,
    currentPage:number,
    offset?:number
}
export default function CPagination({ column, setPage, total, currentPage, offset = 10 }:Props) {

  const pages = Math.ceil(total / offset);
  const page = Math.ceil(currentPage / offset )
  const forward = () => {
    setPage(currentPage + offset)
  }
  const backward = () => {
    setPage(currentPage - offset)
  }

  return (
    <Pagination className="col-span-12 w-full mt-10 self-end">
        <PaginationContent>
        <PaginationPrevious  onClick={backward} />
            {pages <= 5 ?
            <>
                {
                Array.from({length:pages}).map((_, index) => <PageNumber key={index} setPage={setPage} offset={offset} active={page === index + 1}>{`${index + 1}`}</PageNumber>)
                }
            </> :
            page <= 4 ?
            <>
                <PageNumber setPage={setPage} active={page === 1} offset={offset}>1</PageNumber>
                <PageNumber setPage={setPage} active={page === 2} offset={offset}>2</PageNumber>
                <PageNumber setPage={setPage} active={page === 3} offset={offset}>3</PageNumber>
                <PageNumber setPage={setPage} active={page === 4} offset={offset}>4</PageNumber>
                <PaginationItem>
                <PaginationEllipsis />
                </PaginationItem>
                <PageNumber setPage={setPage} active={page === pages} offset={offset}>{`${pages}`}</PageNumber>
            </>:
            page >= pages - 3 ?  
            <>
                <PageNumber setPage={setPage} active={page === 1} offset={offset}>1</PageNumber>
                <PaginationItem>
                <PaginationEllipsis />
                </PaginationItem>
                <PageNumber setPage={setPage} active={page === pages - 3} offset={offset}>{`${pages - 3}`}</PageNumber>
                <PageNumber setPage={setPage} active={page === pages - 2} offset={offset}>{`${pages - 2}`}</PageNumber>
                <PageNumber setPage={setPage} active={page === pages - 1} offset={offset}>{`${pages - 1}`}</PageNumber>
                <PageNumber setPage={setPage} active={page === pages} offset={offset}>{`${pages}`}</PageNumber>

            </>:
            <>
                <PageNumber setPage={setPage} active={page === 1} offset={offset}>1</PageNumber>
                <PaginationItem>
                    <PaginationEllipsis />
                </PaginationItem>
                <PageNumber setPage={setPage} active={page === page - 1} offset={offset}>{`${page - 1}`}</PageNumber>
                <PageNumber setPage={setPage} active={page === page} offset={offset}>{`${page}`}</PageNumber>
                <PageNumber setPage={setPage} active={page === page + 1} offset={offset}>{`${page + 1}`}</PageNumber>
                <PaginationItem>
                    <PaginationEllipsis />
                </PaginationItem>
                <PageNumber setPage={setPage} active={page === pages} offset={offset}>{`${pages}`}</PageNumber>

            </>
            }
            <PaginationNext  onClick={forward} />

        </PaginationContent>
    </Pagination>
  );
}

type PageType = {
    children:string,
    active:boolean,
    offset:number,
    setPage:(a:number) => void
}
const PageNumber = ({
  children,
  active,
  offset,
  setPage
}:PageType) => {

  return (
    <PaginationItem onClick={() => setPage((parseInt(children) - 1) * offset + 1)}>
        <PaginationLink isActive={active}>
            {children}
        </PaginationLink>
    </PaginationItem>

  );
};
