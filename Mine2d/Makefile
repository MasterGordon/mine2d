all: win-64 linux-64

win-64:
	dotnet publish --runtime win-x64 --output build/win-64
	cp credits.md build/win-64
	zip -r build/win-64.zip build/win-64

linux-64:
	dotnet publish --runtime linux-x64 --output build/linux-64
	cp credits.md build/linux-64
	zip -r build/linux-64.zip build/linux-64

clean:
	rm -rf build
