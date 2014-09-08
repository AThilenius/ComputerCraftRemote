idNumber = os.getComputerID()
address = "http://localhost/"
linkAddress = address .. "link/" .. idNumber
commandAddress = address .. "command/" .. idNumber

function Handshake(handshakeValues)
	while true do
		if pcall(function()
				-- Post my pool name, link it with computer ID
				http.post(linkAddress, handshakeValues)
				io.write(http.get(linkAddress).readAll())
			end) then
			-- No Error, return
			return
		end

	end
end

function PumpCommands()
	while true do
		if pcall(function()

				-- Pull next command
				commandRequest = http.get(commandAddress)
				luaCode = commandRequest.readAll()
				retVal = nil

				-- Run it (Within another try catch)
				if pcall (function()
							clientCode = loadstring(luaCode)
							retVal = clientCode()
						end) then
					-- No Error
				else
					-- Error
				end

				-- Post it back to server
				http.post(commandAddress, tostring(retVal))
			end) then
			-- No Error

		else
			-- Error
			return
		end

	end
end

-- Get Startup Info
io.write("Pool Name: ")
poolNameInput = io.read()

handshakeString = 
	"PoolName:" .. poolNameInput .. "\n"

-- Run or re-connect
while true do
	io.write("Attempting to connect to HTTP server\n")
	Handshake(handshakeString)
	io.write("Connected!\n");
	PumpCommands()
	io.write("Lost connection. Reconnecting...\n")
end