import NFTDetailsLeft from "@/components/NFTDetails/NFTDetailLeft"
import NFTDetailsRight from "@/components/NFTDetails/NFTDetailRight"
import NFTProvenance from "@/components/NFTDetails/NFTProvenance"
import MoreFromCollection from "@/components/NFTDetails/MoreFromCollection"
import MoreFromCreator from "@/components/NFTDetails/MoreFromCreator"
type Props = {}

const NFTDetail = (props: Props) => {
  return ( 
    <div className="flex flex-col gap-10">
      <div className='flex flex-col lg:flex-row gap-10'>
          <NFTDetailsLeft/>
          <NFTDetailsRight/>
      </div>
      <NFTProvenance/>
      <MoreFromCollection/>
      <MoreFromCreator/>
    </div>
  )
}

export default NFTDetail