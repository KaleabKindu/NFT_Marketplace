'use client'
import { useState } from 'react'
import { Button } from '../ui/button'
import { tabs } from '@/data'

type Props = {
    tab:string,
    setTab:(a:string) => void
}

const UserTabs = ({tab, setTab}: Props) => {
  return (
    <div className='flex gap-5 mt-16 mb-8'>
        
        {
            tabs.map((_tab, index) => 
            <Button key={index} variant={tab === _tab ? 'default':'ghost'} className='rounded-full capitalize' onClick={() => setTab(_tab)}>{_tab}</Button>
            )
        }
    </div>
  )
}

export default UserTabs