import { Metadata } from 'next/types'
import { SearchInput } from '@/components/collection/SearchFilter'
import UsersList from '@/components/explore/users/UsersList'
export const metadata: Metadata = {
    title: 'Explore All Users | NFT Marketplace',
  }
  
type Props = {}

const Page = (props: Props) => {
  return (
    <div className='flex flex-col gap-10 mt-16'>
      <SearchInput/>
      <UsersList/>
    </div>
  )
}

export default Page