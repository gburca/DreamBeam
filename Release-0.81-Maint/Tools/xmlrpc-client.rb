#!/usr/bin/ruby

require 'xmlrpc/client'
require 'pp'

#server = XMLRPC::Client.new("192.168.1.8", "/DreamBeam", 50000)
#result = server.call("SetContent", "RomCor 75")

identType_Song = 0
identType_BibleVerseIdx = 1
identType_BibleVerseRef = 2
identType_PlainText = 3

identity = {'Type' => 1, 'SongName' => "", 'SongStrophe' => 0,
	'BibleTransl' => "KJV", 'VerseIdx' => 0, 'VerseRef' => "", 'Text' => ""}

identity['Type'] = identType_BibleVerseIdx
identity['BibleTransl'] = "KJV"
identity['VerseIdx'] = 79

#identity['Type'] = identType_PlainText
#identity['Text'] = "The title\n\nFollowed by some slide text\nWith two lines"
#
#identity['Type'] = identType_Song
#identity['SongName'] = "New Song"
#identity['SongStrophe'] = 3
#
#identity['BibleTransl'] = "RomCor"
#identity['Type'] = identType_BibleVerseRef
#identity['VerseRef'] = "Mat 8:5"

server = XMLRPC::Client.new("192.168.1.8", "/DreamBeam", 50000)

begin
	result = server.call("SetContent", identity)
	puts result
rescue XMLRPC::FaultException => e
	puts "Received XML Error:"
	puts e.faultCode
	puts e.faultString
end
