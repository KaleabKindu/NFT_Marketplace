import NFTList from "@/components/assets/NFTList"
import SearchBar from "@/components/assets/SearchBar"
import Filter from "@/components/assets/Filter"

type Props = {}

const Page = (props: Props) => {
  return (
    <div className="flex flex-col gap-10">
        <SearchBar className="mt-[10vh] w-[60%] mx-auto"/>
        <Filter/>
        <NFTList/>
    </div>
  )
}

export default Page