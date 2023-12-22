import CollectionDetail from "@/components/collection/CollectionDetail"
import FilterComponent from "@/components/FilterComponent"
import TopCollections from "@/components/collection/TopCollections"
import CollectionNFTs from "@/components/collection/CollectionNFTs"

type Props = {}

const page = (props: Props) => {
  return ( 
    <div className="flex flex-col gap-10">
        <CollectionDetail/>
        <FilterComponent/>
        <CollectionNFTs/>
        <TopCollections/>
    </div>
  )
}

export default page