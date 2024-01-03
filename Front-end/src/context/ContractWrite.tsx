import { ReactNode, createContext, useEffect, useState } from 'react'
import { parseEther } from 'viem'
import { useContractWrite, usePrepareContractWrite, useWaitForTransaction } from 'wagmi'
import  { address } from '@/../../Blockchain/constants' 
import { useToast } from "@/components/ui/use-toast"
import { ToastAction } from '@/components/ui/toast'
import Link from 'next/link'
import { Routes } from '@/routes'
import abi from '@/abi/MyNFT.json'

interface ContractWriteContextProps {
  isLoading:boolean,
  isError:boolean,
  waitingForTransaction:boolean,
  transactionSuccess:boolean,
  writing:boolean,
  writeSuccess:boolean,
  prepareContractWrite:(a:boolean, b:number) => void,
  prepareArguments:(a:any[]) => void
}

export const ContractWriteContext = createContext<ContractWriteContextProps>({
  isLoading:false,
  isError:false,
  waitingForTransaction:false,
  transactionSuccess:false,
  writing:false,
  writeSuccess:false,
  prepareContractWrite:(a:boolean, b:number) => {},
  prepareArguments:(a:any[]) => {}
})

type Props = {
  children:ReactNode
}

const ContractWrite = ({children}: Props) => {
  const { toast } = useToast()
  const [ prepare, setPrepare ] = useState(false)
  const [ args, setArgs ] = useState<any[]>([])
  const [ value, setValue ] = useState('0')
  const { 
    config, 
    isLoading:preparing, 
    isSuccess:prepared,
    isError:preparingError 
  } = usePrepareContractWrite({
      address:address,
      abi:abi,
      args:args,
      functionName:'mintProduct',
      value:parseEther(value),
      enabled:prepare,
      onError(err) {
        console.log('preparation failed',err)
        toast({
          variant: "destructive",
          title: "Uh oh! Something went wrong.",
          description: err.name,
          action: <ToastAction altText='Try Again'>Try Again</ToastAction>
        })
      },
    })
    const { 
      data, 
      isLoading:writing, 
      isSuccess:writeSuccess, 
      isError:writeError,
      write
     } = useContractWrite(config)

  const { 
    isLoading:waitingForTransaction, 
    isError:transactionError,
    isSuccess:transactionSuccess  
  } = useWaitForTransaction({
      hash: data?.hash,
      onError(err) {
        console.log('transaction failed',err)
        toast({
            variant: "destructive",
            title: "Uh oh! Something went wrong.",
            description: err.name,
            action: <ToastAction altText='Try Again'>Try Again</ToastAction>
        })
      },
      onSuccess(data) {
        toast({
            description: 'Your Transaction was Successfull',
            action: (
                <ToastAction altText="View Transaction">
                    <Link href={`${Routes.ETHER_TRANSACTIONS}/${data.blockHash}`} target='_blank'>
                        View Transaction
                    </Link>
                </ToastAction>
            ),

        })
      },
  })
  useEffect(() => {
      if(prepared){
          write?.()
      }
  },[prepared, write])

  const prepareContractWrite = (a:boolean, value:number) => {
      setPrepare(a)
      setValue(value.toString())
  }
  const prepareArguments = (a:any[]) => setArgs(a)
  const isLoading = writing || preparing || waitingForTransaction
  const isError = writeError || preparingError || transactionError

  return (
    <ContractWriteContext.Provider value={{
          isLoading:isLoading,
          isError:isError,
          waitingForTransaction:waitingForTransaction,
          transactionSuccess:transactionSuccess,
          writing:writing,
          writeSuccess:writeSuccess,
          prepareContractWrite:prepareContractWrite,
          prepareArguments:prepareArguments
      }}>
      {children}
    </ContractWriteContext.Provider>
  )
}

export default ContractWrite