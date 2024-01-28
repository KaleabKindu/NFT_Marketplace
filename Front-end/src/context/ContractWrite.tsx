import { ReactNode, createContext, useEffect, useState } from 'react'
import { parseEther } from 'viem'
import { useContractWrite, usePrepareContractWrite, useWaitForTransaction } from 'wagmi'
import { useToast } from "@/components/ui/use-toast"
import { ToastAction } from '@/components/ui/toast'
import Link from 'next/link'
import { Routes } from '@/routes'
import NftAbi from '@/data/abi/MyNFT.json'

interface ContractWriteContextProps {
  isLoading:boolean,
  isError:boolean,
  waitingForTransaction:boolean,
  transactionSuccess:boolean,
  writing:boolean,
  writeSuccess:boolean,
  contractWrite:(a:string, b?:string, d?:any[]) => void,
}

export const ContractWriteContext = createContext<ContractWriteContextProps>({
  isLoading:false,
  isError:false,
  waitingForTransaction:false,
  transactionSuccess:false,
  writing:false,
  writeSuccess:false,
  contractWrite:(a:string, b?:string, d?:any[]) => {},
})

type Props = {
  children:ReactNode
}

const ContractWrite = ({children}: Props) => {
  const { toast } = useToast()
  const [ prepare, setPrepare ] = useState(false)
  const [ args, setArgs ] = useState<any[] | undefined>(undefined)
  const [ functionName, setFunctionName ] = useState('')
  const [ value, setValue ] = useState<string | undefined>(undefined)
  const { 
    config, 
    isLoading:preparing, 
    isSuccess:prepared,
    isError:preparingError 
  } = usePrepareContractWrite({
      address:process.env.NEXT_PUBLIC_CONTRACT_ADDRESS as `0x${string}`,
      abi:NftAbi,
      args:args,
      functionName:functionName,
      value:value ? parseEther(value):undefined,
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
        console.log('Transaction Success', data)
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
          console.log('ready', args)
      }
  },[prepared, write])
  const contractWrite = (functionName:string, value?:string, args?:any[]) => {
      setPrepare(true)
      setValue(value)
      setFunctionName(functionName)
      setArgs(args)
  }
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
          contractWrite:contractWrite,
      }}>
      {children}
    </ContractWriteContext.Provider>
  )
}

export default ContractWrite