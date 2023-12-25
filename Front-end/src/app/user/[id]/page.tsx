'use client'
import UserDetail from '@/components/user/UserDetail'
import UserTabs from '@/components/user/UserTabs'
import UserNFTs from '@/components/user/UserNFTs'
import { useState } from 'react'
import UserCollections from '@/components/user/UserCollections'
import UserFollowersFollowings from '@/components/user/UserFollowersFollowings'
import PopularUsers from '@/components/user/PopularUsers'
type Props = {}

const Page = (props: Props) => {
  const [ tab, setTab ] = useState('created')

  return (
    <div className='flex flex-col gap-5'>
      <UserDetail/>
      <UserTabs tab={tab} setTab={(a:string) => setTab(a)}/>
      {(tab === 'created' || tab === 'owned' || tab === 'liked' ) && <UserNFTs/>}
      {tab === 'collections' && <UserCollections/>}
      {(tab === 'followers' || tab === 'following') && <UserFollowersFollowings/>}
      <PopularUsers/>
    </div>
  )
}

export default Page