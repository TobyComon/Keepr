import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeep: {},
  vaults: [],
  activeVault: {},
  vaultKeeps: [],
  profile: {},
  profileKeeps: [],
  profileVaults: [],
  myVaults: [],
  isMe: false
})
