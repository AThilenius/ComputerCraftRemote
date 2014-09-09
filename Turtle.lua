idNumber = os.getComputerID()
address = "http://localhost/"
linkAddress = address .. "link/" .. idNumber
commandAddress = address .. "command/" .. idNumber

function Handshake(handshakeValues)
	while true do

		retValue = nil;
		if pcall(function()
				-- Post my pool name, link it with computer ID
				retValue = http.post(linkAddress, handshakeValues)
				--io.write(http.get(linkAddress).readAll())
			end) then

			-- No Throw, check return value
			if (retValue == nil) then
				io.write(".")
			else
				return
			end
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
poolNameInput = "Unassigned"
handshakeString = 
	"PoolName:" .. poolNameInput .. "\n"

-- Check fuel
if turtle.getFuelLevel() < 80 then
	io.write("Please place fuel in slot 0\n")
end

while turtle.getFuelLevel() < 80 do
	os.setComputerLabel("REFUEL")
	while turtle.refuel(16) do end
	os.sleep(0.1)
end

os.setComputerLabel("Disconnected")

-- Run or re-connect
while true do
	io.write("Attempting to connect to HTTP server\n")
	Handshake(handshakeString)
	term.clear()
	term.setCursorPos(1, 1)
	io.write("Connected!\n");
	io.write("This is turtle ID " .. idNumber .. "\n")
	os.setComputerLabel("ID: " .. idNumber)
	PumpCommands()
	io.write("Lost connection. Reconnecting...\n")
	os.setComputerLabel("Disconnected")
end