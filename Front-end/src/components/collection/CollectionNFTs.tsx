'use client' 
import NFTCard from "../explore/assets/NFTCard"
type Props = {}

const CollectionNFTs = (props: Props) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center justify-center gap-5">
      {
        Array.from({length:12}).map((_, index) => 
          <NFTCard key={index}/>
        )
      }
    </div>
  )
}

export default CollectionNFTs